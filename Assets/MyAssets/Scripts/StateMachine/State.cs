using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStateMachine
{

    public abstract class State : IState
    {
        public StateMachine stateMachine;

        public abstract void Exit();

        public abstract void Initialize<T>(T arg);

        public void SetStateMachineParent(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }

}
