using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        IsRootState = true;
        IsMomentumBased = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsFalling", true);
    }
    
    protected override void UpdateState()
    {
        if (Context.PlayerIsLandingJump)
            ApplyGravity(Context.JumpFallGravity);
        else
        {
            ApplyGravity(Context.FallGravity);
            Context.CoyoteTimer -= Time.deltaTime;
        }
        JumpBufferTimer();
        ShouldStateSwitch();
    }
    
    public override void ShouldStateSwitch()
    {
        if (!Context.Input.Rolling && Context.CoyoteTimer > 0.0f && Context.Input.JumpWasPressed)
            SwitchState(Factory.Jump());
        else if (Context.Bounce)
        {
            SwitchState(Factory.Jump());
            Context.WalrusFeedback?.PlayFeedbacks();
        }
        else if (Context.PlayerIsGrounded)
        {
            SwitchState(Factory.Grounded());
            Context.LandingFeedback?.PlayFeedbacks();
        }
    }
    
    protected override void ExitState()
    {
        Context.PlayerIsLandingJump = false;
        Context.Animator.SetBool("IsFalling", false);

    }

    public sealed override void InitializeSubState()
    {
        if (Context.Input.Rolling)
            SetSubState(Factory.Roll());
        else if (Context.Input.Running)
            SetSubState(Factory.Run());
        else if (Context.Input.Moving)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Idle());
    }

    public override void OnAnimationEvent() { }

    private void ApplyGravity(float fallGravity)
    {
        Context.CurrentGravity += fallGravity * Time.deltaTime;
    }

    private void JumpBufferTimer()
    {
        if (Context.Input.JumpWasPressed)
            Context.JumpBufferTimer = Context.JumpBufferTime;
        else
            Context.JumpBufferTimer -= Time.deltaTime;
    }
}