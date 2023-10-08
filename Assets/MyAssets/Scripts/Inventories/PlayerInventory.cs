using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventories;
using static UnityEditor.Progress;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform equippedItemSocket;
    [SerializeField] private float equipTime = 0.5f;
    [SerializeField] private float unequipTime = 0.5f;
    [SerializeField] private Vector3 unequipEndingSpot = new Vector3(0.05f, -0.2f, 0);
    //[SerializeField] private Vector3 unequipEndingRotation = new Vector3(10, 30, 10);

    private Equippable[] equippables;
    private int inventorySize = 3;
    private int currentItemIndex;
    private bool itemIsEquipped;
    private bool equippedCoroutineTargetValue;

    private Equippable CurrentItem => equippables[currentItemIndex];

    private void Start()
    {
        InitializeItems();
    }

    private void InitializeItems()
    {
        equippables = new Equippable[inventorySize];
    }
    
    //Inventory management
    private void UpdgradeInventorySize(int increment)
    {
        DropExcessItems(increment);
        ResizeArray(inventorySize + increment);
    }
    private void DropExcessItems(int increment)
    {
        if (increment < 0)
        {
            for (int i = 0; i > increment; i--)
            {
                DropItemAtIndex(inventorySize - 1 + i);
            }
        }
    }
    private void ResizeArray(int newSize)
    {
        inventorySize = newSize;
        Equippable[] newArray = new Equippable[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            newArray[i] = equippables[i];
        }
        equippables = newArray;
    }

    //Pickup/drop
    public void PickupItem(Equippable item)
    {
        if (CurrentItem == null)
        {
            PickupItemAtIndex(item, currentItemIndex);
            return;
        }
        for (int i = 0; i < inventorySize; i++)
        {
            if (equippables[i] == null)
            {
                PickupItemAtIndex(item,i);
                return;
            }
        }
        PickupItemAtIndex(item, currentItemIndex);
    }
    private void PickupItemAtIndex(Equippable item, int index)
    {
        DropItemAtIndex(index);
        equippables[index] = item;
        item.transform.SetParent(equippedItemSocket);
        item.transform.localPosition = unequipEndingSpot;
        item.transform.localRotation = Quaternion.identity;
        item.gameObject.SetActive(false);
        item.OnPickUp();
        if (index == currentItemIndex)
            EquipItem();
    }
    public void DropCurrentItem()
    {
        DropItemAtIndex(currentItemIndex);
    }
    private void DropItemAtIndex(int index)
    {
        if (!equippables[index] || !itemIsEquipped || !equippedCoroutineTargetValue)
            return;
        itemIsEquipped = equippedCoroutineTargetValue = false;
        equippables[index].transform.SetParent(null);
        equippables[index].OnDrop();
        equippables[index] = null;
    }

    //equipping/unequipping
    public void EquipItem()
    {
        StopAllCoroutines();
        StartCoroutine(EquipItemCoroutine());
    }
    public void UnEquipItem()
    {
        StopAllCoroutines();
        StartCoroutine(UnequipItemCoroutine());
    }
    public IEnumerator EquipItemCoroutine()
    {
        if (itemIsEquipped)
            yield break;
        EventsManager.EquipsItem.Invoke();
        AddListenersToEquippedItem();
        yield return StartCoroutine(EquipAnimationCoroutine(!equippedCoroutineTargetValue));
    }
    public IEnumerator UnequipItemCoroutine()
    {
        if (!itemIsEquipped)
            yield break;
        RemoveListenersToEquippedItem();
        yield return StartCoroutine(EquipAnimationCoroutine(!equippedCoroutineTargetValue));
    }
    private void AddListenersToEquippedItem()
    {
        EventsManager.Action1.AddListener(CurrentItem.OnAction1);
        EventsManager.Action2.AddListener(CurrentItem.OnAction2);
    }
    private void RemoveListenersToEquippedItem()
    {
        EventsManager.Action1.RemoveListener(CurrentItem.OnAction1);
        EventsManager.Action2.RemoveListener(CurrentItem.OnAction2);
    }

    //switching current item
    public void SwitchToItemAtIndex(int index)
    {
        if (index == currentItemIndex)
        {
            //caso in cui ri-seleziono l'index corrente
            if (!CurrentItem)
                return;
            if (itemIsEquipped)
                UnEquipItem();
            else
                EquipItem();
        }
        else
        {
            //caso in cui seleziono nuovo index
            StopAllCoroutines();
            StartCoroutine(SwitchToItemAtIndexCoroutine(index));
        }
    }
    public IEnumerator SwitchToItemAtIndexCoroutine(int index)
    {
        //bool reEquip = itemIsEquipped;
        bool reEquip = true;    //mi sembra più comodo se funziona sempre così
        if (reEquip)
            yield return StartCoroutine(UnequipItemCoroutine());
        currentItemIndex = index;
        if (!CurrentItem)
            yield break;
        if (reEquip)
            yield return StartCoroutine(EquipItemCoroutine());

    }
    public void SwitchToItemAtSlotNumber(int slotNumber)
    {
        SwitchToItemAtIndex(slotNumber - 1);    
    }
    public void SwitchToNextItem()
    {
        int newIndex = currentItemIndex + 1;
        SwitchToItemAtIndex(newIndex >= inventorySize ? 0 : newIndex);
    }
    public void SwitchToPreviousItem()
    {
        int newIndex = currentItemIndex - 1;
        SwitchToItemAtIndex(newIndex < 0 ? inventorySize - 1 : newIndex);
    }

    //external calls
    private void OnStartsHolding()
    {
        UnEquipItem();
    }
    private void DropItemInput()
    {
        DropCurrentItem();
    }

    //animations
    private IEnumerator EquipAnimationCoroutine(bool equipping)
    {
        equippedCoroutineTargetValue = equipping;
        Vector3 targetPos = equipping ? Vector3.zero : unequipEndingSpot;
        float ratio = CurrentItem.transform.localPosition.magnitude / unequipEndingSpot.magnitude;
        float time = (equipping ? equipTime * ratio : unequipTime * (1 - ratio)) ;
        if (equipping)
            CurrentItem.gameObject.SetActive(true);
        for (float timer = 0; timer < time; timer += Time.deltaTime)
        {
            float speed = Vector3.Distance(CurrentItem.transform.localPosition, targetPos) / (time - timer);
            CurrentItem.transform.localPosition = Vector3.MoveTowards(CurrentItem.transform.localPosition, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        if(!equipping)
            CurrentItem.gameObject.SetActive(false);
        itemIsEquipped = equipping;
    }

    //events
    public void SubscribeToEvents()
    {
        EventsManager.NumberPressed.AddListener(SwitchToItemAtSlotNumber);
        EventsManager.NextItemPressed.AddListener(SwitchToNextItem);
        EventsManager.PreviousItemPressed.AddListener(SwitchToPreviousItem);
        EventsManager.StartsHolding.AddListener(OnStartsHolding);
        EventsManager.DropItem.AddListener(DropItemInput);
    }
    public void UnsubscribeToEvents()
    {
        EventsManager.NumberPressed.RemoveListener(SwitchToItemAtSlotNumber);
        EventsManager.NextItemPressed.RemoveListener(SwitchToNextItem);
        EventsManager.PreviousItemPressed.RemoveListener(SwitchToPreviousItem);
        EventsManager.StartsHolding.RemoveListener(OnStartsHolding);
        EventsManager.DropItem.RemoveListener(DropItemInput);
    }

}
