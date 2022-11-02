using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// All player states inherit from this method. This provides a centralized location from which 
/// we can then refactor functions in the event we need them.
/// 
/// </summary>
public abstract class PlayerBaseState : State
{
    // Only children inheriting from this class can access this critical piece of information.
    [SerializeField] protected PlayerStateMachine stateMachine;
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion) * deltaTime);
    }
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

}
