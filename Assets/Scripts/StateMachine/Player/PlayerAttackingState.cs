using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private readonly float CrossFadeDuration = 0.1f;

    private Attack currAttack;
    private bool alreadyAppliedForce;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        currAttack = stateMachine.AttackList[attackIndex];    
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(currAttack.AnimationName, CrossFadeDuration);
    }

    public override void Running(float deltaTime)
    {
        Move(deltaTime);
        float NormalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (NormalizedTime < 1f)
        {
            if (currAttack.DurationOfForce < NormalizedTime)
            {
                TryApplyForce();
            }
        }
        else
        {
            if (stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.SwitchState(new PlayerIdleState(stateMachine));
                return;
            }
            stateMachine.SwitchState(new PlayerMovingState(stateMachine));
            return;
        }    
    }

    public override void Exit()
    {
        
    }

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

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * currAttack.Force);

        alreadyAppliedForce = true;
    }
}
