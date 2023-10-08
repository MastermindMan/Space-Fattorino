using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStateMachine
{

    public interface IStateMachineOwner
    {
        public void InitializeStateMachine(IState startingState);

        public void ChangeState<T>(IState newState, T arg);

    }

}
