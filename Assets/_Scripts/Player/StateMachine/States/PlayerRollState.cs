using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        IsMomentumBased = true;
    }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsRolling", true);
        Context.CurrentRollingSpeed = Context.MovementVector.magnitude < Context.InitialRollingSpeed ? Context.InitialRollingSpeed : Context.MovementVector.magnitude;
        Context.RollFeedback?.PlayFeedbacks();
        Context.RollContinuousImpulse.Active = true;
    }

    protected override void UpdateState()
    {
        LookTowardsCameraForwardVector();
        Context.MovementVector = Context.BearTransform.forward * CalculateCurrentRollingSpeed();
        Context.Animator.speed = Context.CurrentRollingSpeed / (3.0f * Mathf.PI);
        var impulseStrength = CalculateCurrentRollingSpeed().Remap(0, Context.MaxRollingSpeed, Context.ImpulseStrengthRange.x, Context.ImpulseStrengthRange.y);
        Context.RollContinuousImpulse.UpdateImpulseStrength(impulseStrength);
        ShouldStateSwitch();
    }
    
    public override void ShouldStateSwitch()
    {
        switch (Context.Input.Rolling)
        {
            case false when Context.Input.Running:
                SwitchState(Factory.Run());
                break;
            case false when Context.Input.Moving:
                SwitchState(Factory.Walk());
                break;
            case false:
                SwitchState(Factory.Idle());
                break;
        }
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsRolling", false);
        Context.CurrentRollingSpeed = 0.0f;
        Context.Animator.speed = 1.0f;
        Context.RollFeedback?.ResumeFeedbacks();
        Context.RollContinuousImpulse.Active = false;
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAnimationEvent() { }

    private void LookTowardsCameraForwardVector()
    {
        Context.BearTransform.forward = Vector3.Slerp(Context.BearTransform.forward, Context.MainCameraForward, Context.LookRotationSpeed * Time.deltaTime);
    }

    private float CalculateCurrentRollingSpeed()
    {
        switch (Context.RelativeSlopeAngle)
        {
            case < -1.0f when Context.CurrentRollingSpeed < Context.MaxRollingSpeed:
                Context.CurrentRollingSpeed += Context.AccelerationRate * Time.deltaTime;
                break;
            case > 1.0f when Context.CurrentRollingSpeed > 0.0f:
                Context.CurrentRollingSpeed -= Context.DecelerationRate * Time.deltaTime;
                break;
            case > 1.0f:
                Context.CurrentRollingSpeed = 0.0f;
                break;
            default:
                Context.CurrentRollingSpeed = Context.CurrentRollingSpeed;
                break;
        }
        return Context.CurrentRollingSpeed;
    }
}