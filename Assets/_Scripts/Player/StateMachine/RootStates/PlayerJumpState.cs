using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int _isJumping = Animator.StringToHash("IsJumping");

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Context.Animator.SetBool(_isJumping, true);
        Context.CurrentGravity = Context.InitialVelocity;
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT STATE: PlayerJumpState");
        Context.MovementVectorY = HandleJump();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool(_isJumping, false);
    }

    public override void ShouldStateSwitch()
    {
        if (Context.PlayerIsGrounded && Context.MovementVectorY < 0.0f)
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
    
    private float HandleJump()
    {
        Context.PlayerFallTimer -= Time.fixedDeltaTime;
        if (Context.PlayerFallTimer < 0.0f)
        {
            if (Context.MovementVectorY >= 0.0f)
            {
                Context.CurrentGravity -= Context.RiseDecrementAmount;
                Context.PlayerFallTimer = Context.IncrementFrequency;
                Context.AppliedGravity = Context.CurrentGravity;
            }
            else
                HandleFall();
        }
        return Context.AppliedGravity;
    }
    
    private void HandleFall()
    {
        if (Context.CurrentGravity > Context.MaximumGravity)
        {
            Context.CurrentGravity += Context.FallIncrementAmount;
        }
        Context.PlayerFallTimer = Context.IncrementFrequency;
        Context.AppliedGravity = Context.CurrentGravity;
    }
}
