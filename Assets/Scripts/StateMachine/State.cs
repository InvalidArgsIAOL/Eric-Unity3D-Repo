using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    #region Base State Logic

    /// <summary>
    /// Enter method of states. Runs at the start of a state.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Body method of states. Continuously runs until StateMachine replaces current state with another state.
    /// Takes param deltaTime for Vectors and smoothing (camera, movement, etc).
    /// </summary>
    /// <param name="deltaTime"></param>
    public abstract void Running(float deltaTime);

    public abstract void Exit();
    #endregion
}
