using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Idle state. The player is not moving in this state and no input is being read.
/// 
/// This is a test class at this time. The current model does not have attacking animations.
/// As such, we may need to refactor this eventually.
/// 
/// DevNotes: From this class, I've figured out a way to:
/// Cycle between two different animation sets
/// 
/// Needs - requires camera and movement controls. 
/// 
/// IdleState will become FreeMoveLookState - since Idle is only a small part of the entire problem.
/// 
/// </summary>
public class PlayerIdleState : PlayerBaseState
{
    #region Animation Fields

    private readonly int IdleHash = Animator.StringToHash("Idle"); // Hashing so we don't have to write strings everywhere.

    private readonly int ExhaustedHash = Animator.StringToHash("Exhausted"); // Hashing so we don't have to write strings everywhere.

    private float CrossFadeDuration = 0.1f;

    private float LastNormalizedTime; // Used for transitioning between long idle and shortidle

    // Swap Animation Fields

    private float TimeTilAnimationSwap = 15f; // Should pull this out somewhere

    private List<string> altIdleAnims = new List<string>() { "Idle1", "Idle2", "Idle3" }; // Other idle states - we don't hash these because they're here already.

    private bool AnimationSwapDone = false; // If false, we are okay to execute a altIdle once the time til AnimSwap is complete!

    // Exhausted after doing stuff
    private bool isTired = false;

    #endregion

    public PlayerIdleState(PlayerStateMachine stateMachine, bool isTired = false) : base(stateMachine) 
    {
        this.isTired = isTired;
    }

    public override void Enter()
    {
        if (isTired) // If we are exhausted, play this animation instead.
        {
            stateMachine.Animator.CrossFadeInFixedTime(ExhaustedHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(IdleHash, CrossFadeDuration);
        }
    }

    public override void Running(float deltaTime)
    {
        Vector2 isMoving = stateMachine.InputReader.MovementValue;

        if (isMoving != Vector2.zero)
        {
            stateMachine.SwitchState(new PlayerMovingState(stateMachine));
            return;
        }

        LastNormalizedTime = GetNormalizedTime(stateMachine.Animator, "Idle"); // The tag for all idle states is Idle.

        IdleAnimationSwap(deltaTime);

    }

    public override void Exit()
    {
        Debug.Log("Exiting");
    }


    /// <summary>
    /// 
    /// DevNote: This was the product of a few tutorials and a lot of research into Animator.
    /// 
    /// Extremely important information to make transitions smoother and prevent weird issues
    /// where you end up cutting off animations. Will be abstracted later to the PlayerBaseState.
    /// 
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0); // Current info.
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0); // Next animation's info.

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag)) // If we are transitioning
        {
            return nextInfo.normalizedTime; // We are closer to the next state, and should normalize based on it.
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag)) // Otherwise, we're still in an animation
        {
            return currentInfo.normalizedTime; // So return the current normalized time
        }
        else
        {
            return 0f; // Exception Catching - prevents situations where we may input 
        }
    }

    /// <summary>
    /// 
    /// This function takes in deltaTime and checks to see if we want to change the idle animation.
    /// I have it coded in manually to change to a different idle every 15f seconds.
    /// Logic in comments.
    /// 
    /// </summary>
    /// <param name="deltaTime"></param>
    private void IdleAnimationSwap(float deltaTime)
    {
        if (TimeTilAnimationSwap <= 0f && !AnimationSwapDone) // If we meet the conditions for a swap (Time is 0, and we haven't yet done a special animation)
        {
            if (LastNormalizedTime > 1.0f) // Once we have finished playing the other animations...
            {
                // CrossFade to make the animation nicer.
                stateMachine.Animator.CrossFadeInFixedTime(altIdleAnims[Random.Range(0, 3)], CrossFadeDuration);

                // We have done a special animation - set this to true to prevent spamming the special animation.
                AnimationSwapDone = true;

                // We reset the timer here.
                TimeTilAnimationSwap = 15f;
            }
        }
        else // For all other situations...
        {
            
            if (LastNormalizedTime > 1.0f) // If we have ended the last animation...
            {
                // CrossFade to the next animation.
                stateMachine.Animator.CrossFadeInFixedTime(IdleHash, CrossFadeDuration);

                // Now that we're back in the original animation, we can now allow the alt. Animation to begin counting again.
                AnimationSwapDone = false;
            }

            if (!AnimationSwapDone) // We only start counting down if AnimationSwapDone is false! Otherwise, we will tick down during the alternative animations!
            {
                TimeTilAnimationSwap -= deltaTime;
            }
        }
    }
}
