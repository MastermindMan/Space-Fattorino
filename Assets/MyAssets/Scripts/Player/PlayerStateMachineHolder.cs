using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStateMachine;
using MyStateMachine.PlayerStates;

public enum PlayerStatesEnum { standard, driving, ENUM_LENGHT };

public class PlayerStateMachineHolder : MonoBehaviour, IStateMachineOwner
{
    private StateMachine stateMachine;
    private IState[] statesContainer;


    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        InitializeStates();
        InitializeStateMachine(GetState(PlayerStatesEnum.standard));
    }
    private void InitializeStates()
    {
        statesContainer = new IState[(int)PlayerStatesEnum.ENUM_LENGHT];

        statesContainer[(int)PlayerStatesEnum.standard] = new Standard();
        statesContainer[(int)PlayerStatesEnum.driving] = new Driving();
    }
    public void InitializeStateMachine(IState startingState)
    {
        stateMachine = new StateMachine(startingState);
    }

    private IState GetState(PlayerStatesEnum state)
    {
        return statesContainer[(int)state];
    }

    public void ChangeState<T>(IState newState, T arg)
    {
        stateMachine.ChangeState(newState, arg);
    }
    public void ChangeState<T>(PlayerStatesEnum newState, T arg)
    {
        ChangeState(GetState(newState), arg);
    }
    public void ChangeState(IState newState)
    {
        stateMachine.ChangeState(newState, 0);
    }
    public void ChangeState(PlayerStatesEnum newState)
    {
        ChangeState(GetState(newState), 0);
    }
    /*public void ChangeState<T>(T arg)
    {
        foreach (IState state in statesContainer)
        {
            if (state is T)
            {
                ChangeState(state, arg);
                return;
            }
        }
        Debug.LogWarning("This statemachine does not contain IState deriving classes of the requested type");
    }*/

}
