using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 
/// PlayerStateMachine. Likely will have a lot of fields. Regions separate code.
/// Looking into compressing and refactoring at some point.
/// 
/// </summary>
public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; internal set; }

    [field: SerializeField] public CharacterController Controller { get; internal set; }

    [field: SerializeField] public InputReader InputReader { get; internal set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public float RotationBuffer { get; internal set; } // Tutorial - used for damping the movement of the Cinemachine Camera

    [field: SerializeField] public ForceReceiver ForceReceiver { get; internal set; }



    [field: SerializeField] public Attack[] AttackList { get; internal set; } // List in case we want to expand to more attacks
    public Transform MainCameraTransform { get; private set; }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // snaps to center of the screen.

        Cursor.visible = false;

        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerIdleState(this));
    }
}
