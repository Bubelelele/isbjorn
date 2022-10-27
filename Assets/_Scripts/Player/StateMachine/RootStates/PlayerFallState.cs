using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int _isFalling = Animator.StringToHash("IsFalling");

    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Context.Animator.SetBool(_isFalling, true);
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT STATE: PlayerFallState");
        CoyoteTimer();
        Context.MovementVectorY = HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool(_isFalling, false);
    }

    public override void ShouldStateSwitch()
    {
        if (Context.CoyoteTimer > 0.0f && Context.Input.JumpIsPressed)
            SwitchState(Factory.Jump());
        else if (Context.PlayerIsGrounded)
            SwitchState(Factory.Grounded());
    }

    public sealed override void InitializeSubState()
    {
        if (!Context.Input.MoveIsPressed)
            SetSubState(Factory.Idle());
        else if (Context.Input.RunIsPressed)
            SetSubState(Factory.Run());
        else
            SetSubState(Factory.Walk());
    }

    private float HandleGravity()
    {
        Context.PlayerInAirTimer -= Time.fixedDeltaTime;
        if (Context.PlayerInAirTimer < 0.0f)
        {
            if (Context.CurrentGravity > Context.MaximumGravity)
            {
                Context.CurrentGravity += Context.IncrementAmount;
            }
            Context.PlayerInAirTimer = Context.IncrementFrequency;
            Context.AppliedGravity = Context.CurrentGravity;
        }
        return Context.AppliedGravity;
    }

    private void CoyoteTimer()
    {
        Context.CoyoteTimer -= Time.fixedDeltaTime;
    }
}
