using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStateMachine
{
    public interface IState
    {
        public void SetStateMachineParent(StateMachine stateMachine);

        public void Initialize<T>(T arg);

        public void Exit();

    }

}
