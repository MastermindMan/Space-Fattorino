using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventories;

public class Inventory
{
    
    [SerializeField] private List<Equippable> items = new List<Equippable>();
    [SerializeField] private int capacity = int.MaxValue;

    public Inventory() { }
    public Inventory(int capacity)
    {
        this.capacity = capacity;
    }

    public void SetInventoryCapacity(int capacity)
    {
        if (capacity < items.Count)
            Debug.LogWarning("Size is smaller than quantity of contained items");
        this.capacity = capacity;
    }

    public virtual void AddItem(Equippable item)
    {
        if (items.Count + 1 > capacity)
            return;
        items.Add(item);
    }

    //così va solo a istanze, non id...
    public virtual Equippable RetrieveItem(Equippable item)
    {
        if(!items.Contains(item))
            return null;
        items.Remove(item);
        return item;
    }

}
