using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStateMachine
{

    public class StateMachine
    {
        private IStateMachineOwner owner;
        private IState currentState;

        public IStateMachineOwner Owner => owner;
        public IState CurrentState => currentState;

        public StateMachine(IState startingState)
        {
            ChangeState(startingState, 0);
        }

        public void ChangeState<T>(IState newState, T arg)
        {
            if (currentState != null)
            {
                currentState.Exit();
                currentState.SetStateMachineParent(null);
            }
            currentState = newState;
            currentState.SetStateMachineParent(this);
            currentState.Initialize(arg);
        }

    }

}
