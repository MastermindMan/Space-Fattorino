using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStateMachine.PlayerStates
{
    public class Standard : State
    {
        public override void Exit()
        {
            Player.PlayerMovement.UnsubscribeToEvents();
            Player.PlayerInventory.UnsubscribeToEvents();
        }

        public override void Initialize<T>(T arg)
        {
            Player.PlayerInventory.SubscribeToEvents();
            Player.PlayerMovement.SubscribeToEvents();
            Player.PlayerMovement.SetCharacterControllerEnabledValue(true);
            Player.PlayerMovement.SetVerticalFreezedValue(false);
        }

    }
}
