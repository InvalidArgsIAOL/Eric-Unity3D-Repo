using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

/// <summary>
/// 
/// This class handles the input. It inherits an Interface from PlayerControls.
/// The PlayerControls class is a Unity generated class for inputs.
/// 
/// </summary>
public class InputReader : MonoBehaviour, PlayerControls.IPlayerActions
{
    public bool IsAttacking;

    public event Action DodgeEvent;

    public Vector2 MovementValue; // This is input in the cardinal directions for KB+M.

    private PlayerControls _controls; // Implement the Interface, store reference to controls

    /// <summary>
    /// 
    /// This handles initial setup to allow the InputReader to handle the heavy lifting.
    /// 
    /// </summary>
    void Start()
    {
        // We create a new controls for the player [Expandability if multiplayer]
        _controls = new PlayerControls();

        // Set callbacks for these controls to this specific player
        _controls.Player.SetCallbacks(this); 

        // Enable the controls
        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Nothing needs to be done here
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // We are obtaining the movement direction vector here.
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DodgeEvent?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }
}
