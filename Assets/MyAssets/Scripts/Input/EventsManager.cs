using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsManager
{
    //Inputs
    public static UnityEvent<Vector2> MovementDirection = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> MouseMovement = new UnityEvent<Vector2>();
    public static UnityEvent InputDirectionRelease = new UnityEvent();
    public static UnityEvent Interact = new UnityEvent();
    public static UnityEvent EndsInteract = new UnityEvent();
    public static UnityEvent DropItem = new UnityEvent();
    public static UnityEvent Jump = new UnityEvent();
    public static UnityEvent<int> NumberPressed = new UnityEvent<int>();
    public static UnityEvent NextItemPressed = new UnityEvent();    //per controller
    public static UnityEvent PreviousItemPressed = new UnityEvent();    //per controller
    public static UnityEvent Action1 = new UnityEvent();
    public static UnityEvent Action2 = new UnityEvent();

    //Other
    public static UnityEvent StartsHolding = new UnityEvent();
    public static UnityEvent EquipsItem = new UnityEvent();
    public static UnityEvent<Interactable> StartsLookingAtInteractable = new UnityEvent<Interactable>();
    public static UnityEvent<Interactable> StopsLookingAtInteractable = new UnityEvent<Interactable>();
    public static UnityEvent InstantInteracted = new UnityEvent();
    public static UnityEvent<float> InteractionCompletion = new UnityEvent<float>();

    #region ADD LISTENERS

    public static void SubscribeToEvent<T>(List<(UnityEvent<T>, UnityAction<T>)> eventAndCallsList)
    {
        foreach (var couple in eventAndCallsList)
        {
            couple.Item1.AddListener(couple.Item2);
        }
    }
    public static void SubscribeToEvent(List<(UnityEvent, UnityAction)> eventAndCallsList)
    {
        foreach (var couple in eventAndCallsList)
        {
            couple.Item1.AddListener(couple.Item2);
        }
    }
    public static void SubscribeToEvent<T>(UnityEvent<T> eventTarget, UnityAction<T> methodInvoked)
    {
        eventTarget.AddListener(methodInvoked);
    }
    public static void SubscribeToEvent(UnityEvent eventTarget, UnityAction methodInvoked)
    {
        eventTarget.AddListener(methodInvoked);
    }

    #endregion

    #region REMOVE LISTENERS

    public static void UnsubscribeToEvent<T>(List<(UnityEvent<T>, UnityAction<T>)> eventAndCallsList)
    {
        foreach (var couple in eventAndCallsList)
        {
            couple.Item1.RemoveListener(couple.Item2);
        }
    }
    public static void UnsubscribeToEvent(List<(UnityEvent, UnityAction)> eventAndCallsList)
    {
        foreach (var couple in eventAndCallsList)
        {
            couple.Item1.RemoveListener(couple.Item2);
        }
    }
    public static void UnsubscribeToEvent<T>(UnityEvent<T> eventTarget, UnityAction<T> methodInvoked)
    {
        eventTarget.RemoveListener(methodInvoked);
    }
    public static void UnsubscribeToEvent(UnityEvent eventTarget, UnityAction methodInvoked)
    {
        eventTarget.RemoveListener(methodInvoked);
    }

    #endregion

}
