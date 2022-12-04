using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsRolling", true);
        Context.CurrentRollingSpeed = Context.MovementVector.magnitude < Context.InitialRollingSpeed ? Context.InitialRollingSpeed : Context.MovementVector.magnitude;
        Context.RollFeedback?.PlayFeedbacks();
        Context.RollShakeFeedback?.PlayFeedbacks();
    }

    protected override void UpdateState()
    {
        OverrideInput();
        Context.MovementVector = Context.BearTransform.forward * CalculateCurrentRollingSpeed();
        if (Context.Animator.GetCurrentAnimatorStateInfo(0).IsName("RollingLoop"))
            Context.Animator.speed = Context.CurrentRollingSpeed / (3.0f * Mathf.PI);
        ShouldStateSwitch();
    }
    
    public override void ShouldStateSwitch()
    {
        switch (Context.Input.RollIsPressed)
        {
            case false when Context.Input.RunIsPressed:
                SwitchState(Factory.Run());
                break;
            case false when Context.Input.MoveIsPressed:
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
        Context.RollShakeFeedback?.StopFeedbacks();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    private void OverrideInput()
    {
        Context.MovementVector = Vector3.zero;
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
