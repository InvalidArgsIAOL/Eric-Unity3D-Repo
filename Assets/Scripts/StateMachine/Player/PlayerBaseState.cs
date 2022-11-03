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

    protected void Move(float deltaTime) // Overloaded class - used for when we are doing animations that still need forces
    {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion+stateMachine.ForceReceiver.Movement) * deltaTime); // When we're moving with a specific motion vector
    }

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

}
