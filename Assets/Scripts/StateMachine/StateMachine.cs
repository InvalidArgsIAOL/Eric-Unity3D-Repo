using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// StateMachine base. Used to construct other statemachines.
/// 
/// </summary>
public class StateMachine : MonoBehaviour
{
    // State we are currently in.
    private State currentState;

    private void Update()
    {
        // Every frame, call the running method of the currentState.
        if (currentState != null)
        {
            currentState.Running(Time.deltaTime); // another variant with currentState?.Running(Time.deltaTime);
        }
    }

    /// <summary>
    /// 
    /// How StateMachines can switch states. SwitchState takes a State as an argument.
    /// It calls Exit() on the initial state, assigns currentState to the newState, and enters.
    /// Null checks because we may be in a situation where we can have no state (especially during transitions/uncaught exceptions).
    /// 
    /// </summary>
    /// <param name="newState"></param>
    public void SwitchState(State newState)
    {
        if (currentState != null)
        {
            currentState?.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState?.Enter();
        }
    }
}
