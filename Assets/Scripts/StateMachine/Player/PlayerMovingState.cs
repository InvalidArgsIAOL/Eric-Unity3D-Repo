using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// DevNotes: Comment this state. Remember to write that much of this method is taken from tutorials.
/// 
/// </summary>
public class PlayerMovingState : PlayerBaseState
{
    private readonly int MovingBlendTreeHash =  Animator.StringToHash("Moving");

    private readonly int MovingSpeedHash = Animator.StringToHash("MovingSpeed");

    private readonly float AnimatorBuffer = 0.1f;

    private readonly float CrossFadeDuration = 0.1f;

    public PlayerMovingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.SetFloat(MovingSpeedHash, 0f); // Prevents weird behavior from player animator being in a weird state prior to returning to freelook

        stateMachine.Animator.CrossFadeInFixedTime(MovingBlendTreeHash, CrossFadeDuration);
    }

    public override void Running(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.MovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(MovingSpeedHash, 1, AnimatorBuffer, deltaTime);
        FaceMovementDirection(movement, deltaTime); // Way to rotate player when moving.
    }

    public override void Exit()
    {
        // Nothing here yet
    }

    
    /// <summary>
    /// Used to face the right movement direction.
    /// </summary>
    /// <param name="movement"></param>
    /// <param name="deltaTime"></param>
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation =
            Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationBuffer);
    }

    /// <summary>
    /// 
    /// Generic Movement Function - built referencing tutorials and UnityStandardAssets 2018
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateMovement()
    {
        Vector3 camForward = stateMachine.MainCameraTransform.forward;
        Vector3 camRight = stateMachine.MainCameraTransform.right;

        camForward.y = 0f; // We don't care about the camera dealing with verticals, only horizontals
        camRight.y = 0f;  // Remember that vector3 are floats

        camForward.Normalize();
        camRight.Normalize();

        return camForward * stateMachine.InputReader.MovementValue.y +
            camRight * stateMachine.InputReader.MovementValue.x;
    }
}
