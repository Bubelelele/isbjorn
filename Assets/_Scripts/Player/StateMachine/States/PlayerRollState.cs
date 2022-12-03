using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    private readonly int _isRolling = Animator.StringToHash("IsRolling");
    
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Context.Animator.SetBool(_isRolling, true);
        Context.CurrentRollingSpeed = Context.InitialRollingSpeed;
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
        if (!Context.Input.RollIsPressed)
            SwitchState(Factory.Idle());
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool(_isRolling, false);
        Context.CurrentRollingSpeed = 0.0f;
        Context.Animator.speed = 1.0f;
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
