using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class InputEventCaller : MonoBehaviour
{
    const string mouseAxisX = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string mouseAxisY = "Mouse Y";
    const string horizontal = "Horizontal";
    const string vertical = "Vertical";

    private Vector2 directionInput;
    private bool directionHeld;
    Vector2 mouseMovement;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        //movement
        DirectionInput();
        MouseMovementInput();
        JumpInput();
        //interaction
        ActionInput();
        InteractInput();
        //inventory
        SwapItemInput();
        DropItemInput();
    }

    //Inputs
    private void DirectionInput()
    {
        GetAxisValues(out directionInput, horizontal, vertical);
        if (directionInput.magnitude > 0)
        {
            directionHeld = true;
            EventsManager.MovementDirection.Invoke(directionInput);
        }
        else
        {
            if (directionHeld)
            {
                EventsManager.InputDirectionRelease.Invoke();
            }
            directionHeld = false;
        }
    }
    private void MouseMovementInput()
    {
        GetAxisValues(out mouseMovement, mouseAxisX, mouseAxisY);
        if (mouseMovement.magnitude > 0)
            EventsManager.MouseMovement.Invoke(mouseMovement);
    }
    private void ActionInput()
    {
        GenericKeyDownInput(Settings.action1Key, EventsManager.Action1);
        GenericKeyDownInput(Settings.action2Key, EventsManager.Action2);
    }
    private void InteractInput()
    {
        if (Input.GetKeyDown(Settings.interactKey))
            EventsManager.Interact.Invoke();
        else if (Input.GetKeyUp(Settings.interactKey))
            EventsManager.EndsInteract.Invoke();
    }
    private void JumpInput()
    {
        GenericKeyDownInput(Settings.jumpKey, EventsManager.Jump);
    }
    private void SwapItemInput()
    {
        if (Input.GetKeyDown(Settings.alpha1Key) || Input.GetKeyDown(Settings.keypad1Key))
        {
            EventsManager.NumberPressed.Invoke(1);
        }
        else if (Input.GetKeyDown(Settings.alpha2Key) || Input.GetKeyDown(Settings.keypad2Key))
        {
            EventsManager.NumberPressed.Invoke(2);
        }
        else if (Input.GetKeyDown(Settings.alpha3Key) || Input.GetKeyDown(Settings.keypad3Key))
        {
            EventsManager.NumberPressed.Invoke(3);
        }
    }
    private void DropItemInput()
    {
        GenericKeyDownInput(Settings.dropItemKey, EventsManager.DropItem);
    }

    //Secific supports
    private void GetAxisValues(out Vector2 axisValues, string xName, string yName)
    {
        axisValues.x = Input.GetAxis(xName);
        axisValues.y = Input.GetAxis(yName);
    }
    //Generics supports
    private bool GenericKeyDownInput(KeyCode key, UnityEvent evento)
    {
        if (!Input.GetKeyDown(key))
            return false;
        evento.Invoke();
        return true;
    }
    private bool GenericKeyUpInput(KeyCode key, UnityEvent evento)
    {
        if (!Input.GetKeyUp(key))
            return false;
        evento.Invoke();
        return true;
    }


}
