using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Inventories;
using UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInteraction : MonoBehaviour
{
    private const float MAX_INTERACTION_DISTANCE = 2.0f;
    public enum InteractionMode { standard, holdingObject };

    [SerializeField] private Transform heldInteractableSocket;

    private InteractionMode interactionMode = InteractionMode.standard ;
    private RaycastHit hitInfo;
    private Interactable hitInteractable;
    private Ray ray = new Ray();
    private Holdable heldObject;

    private bool IsLookingAtInteractable => hitInteractable != null;
    private bool IsHoldingInteractable => heldObject != null;
    private bool IsHolding => heldObject != null;
    public RaycastHit HitInfo => hitInfo;
    private bool RaycastCheckActive => interactionMode == InteractionMode.standard;


    void Start()
    {
        SubscribeToEvents();
    }
    private void FixedUpdate()
    {
        FindInteractable();
    }

    private void FindInteractable()
    {
        if (!RaycastCheckActive)
            return;
        ray.origin = Player.CameraControl.transform.position;
        ray.direction = Player.CameraControl.transform.forward;
        Physics.Raycast(ray, out hitInfo, MAX_INTERACTION_DISTANCE, Layers.Interactables, QueryTriggerInteraction.Collide);
        SetHitInteractable(hitInfo.collider ? hitInfo.collider.transform.GetComponent<Interactable>() : null);
    }
    private void SetHitInteractable(Interactable value)
    {
        if (hitInteractable == value)
            return;
        if(hitInteractable)
            EventsManager.StopsLookingAtInteractable.Invoke(hitInteractable);
        hitInteractable = value;
        if (hitInteractable != null)
        {
            EventsManager.StartsLookingAtInteractable.Invoke(hitInteractable);
        }
    }

    private void Interact()
    {
        switch (interactionMode)
        {
            case InteractionMode.standard:
                StandardInteraction();
                break;
            case InteractionMode.holdingObject:
                StopHolding();
                break;
        }
    }
    private void StandardInteraction()
    {
        if(IsLookingAtInteractable)
            hitInteractable.OnInteractionKeyDown();
    }
    private void StopInteract()
    {
        switch (interactionMode)
        {
            case InteractionMode.standard:
                StandardEndInteraction();
                break;
        }
    }
    private void StandardEndInteraction()
    {
        if (IsLookingAtInteractable)
            hitInteractable.OnInteractionKeyUp();
    }

    public void Hold(Holdable holdable)
    {
        heldObject = holdable;
        heldObject.OnHoldingStart();
        SetInteractionMode(InteractionMode.holdingObject);
        StartCoroutine(DragHeldObject());
        EventsManager.StartsHolding.Invoke();
        EventsManager.Action1.AddListener(OnAction1Input);
        EventsManager.DropItem.AddListener(OnDropInput);
    }
    public void StopHolding()
    {
        EventsManager.Action1.RemoveListener(OnAction1Input);
        EventsManager.DropItem.RemoveListener(OnDropInput);
        heldObject.OnHoldingEnd();
        heldObject = null;
        SetInteractionMode(InteractionMode.standard);
    }
    public void SetInteractionMode(InteractionMode newMode)
    {
        interactionMode = newMode;
    }
    private void OnInventoryItemGetsEquipped()
    {
        if (IsHoldingInteractable)
            StopHolding();
    }
    private void OnAction1Input()
    {
        if (heldObject is not Equippable)
            return;
        Equippable temp = (Equippable)heldObject;
        StopHolding();
        Player.PlayerInventory.PickupItem(temp);
    }
    private void OnDropInput()
    {
        StopHolding();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Interact.AddListener(Interact);
        EventsManager.EndsInteract.AddListener(StopInteract);
        EventsManager.EquipsItem.AddListener(OnInventoryItemGetsEquipped);
    }
    private void UnsubscribeToEvents()
    {
        EventsManager.Interact.RemoveListener(Interact);
        EventsManager.EquipsItem.RemoveListener(OnInventoryItemGetsEquipped);
    }


    private IEnumerator DragHeldObject()
    {
        Vector3 fowardLookingLocalDirection = heldInteractableSocket.transform.InverseTransformDirection(heldObject.transform.forward);
        while (IsHolding)
        {
            heldObject.Rigidbody.MovePosition(heldInteractableSocket.transform.position);
            heldObject.transform.rotation = Quaternion.LookRotation(heldInteractableSocket.transform.TransformDirection(fowardLookingLocalDirection));
            yield return new WaitForFixedUpdate();
        }
    }

}
