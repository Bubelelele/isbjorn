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
        //Debug.Log("Entered fall state.");
    }

    protected override void UpdateState()
    {
        // Debug.LogWarning("CURRENT STATE: PlayerFallState");
        CoyoteTimer();
        Context.MovementVectorY = Context.IsLandingJump ? HandleFall() : HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.IsLandingJump = false;
        Context.Animator.SetBool(_isFalling, false);
    }

    public override void ShouldStateSwitch()
    {
        if (Context.CoyoteTimer > 0.0f && Context.Input.JumpIsPressed)
            SwitchState(Factory.Jump());
        else if (Context.LandedOnWalrus)
        {
            SwitchState(Factory.Jump());
        }
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
            Context.BounceVelocity = -Context.AppliedGravity;
        }
        return Context.AppliedGravity;
    }

    private float HandleFall()
    {
        Context.PlayerInAirTimer -= Time.fixedDeltaTime;
        if (Context.PlayerInAirTimer < 0.0f)
        {
            if (Context.CurrentGravity > Context.MaximumGravity)
            {
                Context.CurrentGravity += Context.FallIncrementAmount;
            }
            Context.PlayerInAirTimer = Context.IncrementFrequency;
            Context.AppliedGravity = Context.CurrentGravity;
            Context.BounceVelocity = -Context.AppliedGravity;
        }
        return Context.AppliedGravity;
    }

    private void CoyoteTimer()
    {
        Context.CoyoteTimer -= Time.fixedDeltaTime;
        if (Context.CoyoteTimer > 0.0f) return;
        Context.Animator.SetBool(_isFalling, true);
        Context.CoyoteTimer = 0.0f;
    }
}
