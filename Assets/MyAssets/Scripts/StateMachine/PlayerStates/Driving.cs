using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vehicles;

namespace MyStateMachine.PlayerStates
{
    public class Driving : State
    {
        private Vehicle vehicle;

        public override void Exit()
        {
            vehicle.UnSubscribeToEvents();
        }

        public override void Initialize<T>(T arg)
        {
            vehicle = arg as Vehicle;
            Player.PlayerMovement.SetCharacterControllerEnabledValue(false);
            Player.PlayerMovement.SetVerticalFreezedValue(true);
            vehicle.SubscribeToEvents();
        }
    }

}
