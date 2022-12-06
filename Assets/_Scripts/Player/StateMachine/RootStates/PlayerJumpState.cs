using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log(Context.Bounce);
        Context.CurrentGravity = Context.Bounce ? Context.InitialJumpVelocity * 2.0f - Context.CurrentGravity * 0.5f: Context.InitialJumpVelocity;
        Context.JumpBufferTimer = 0.0f;
        Context.CoyoteTimer = 0.0f;
        Context.Input.JumpWasPressed = false;
        Context.Animator.SetBool("IsJumping", true);
        Context.JumpFeedback?.PlayFeedbacks();
    }

    protected override void UpdateState()
    {
        Rise();
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.CurrentGravity >= 0.0f && Context.Input.JumpIsHeld) return;
        Context.CurrentGravity = 0.0f;
        Context.PlayerIsLandingJump = true;
        SwitchState(Factory.Fall());
    }

    protected override void ExitState()
    {
        Context.Bounce = false;
        Context.Animator.SetBool("IsJumping", false);
    }
    
    public sealed override void InitializeSubState()
    {
        if (Context.Input.Rolling)
            SwitchState(Factory.Roll());
        else if (Context.Input.Running)
            SetSubState(Factory.Run());
        else if (Context.Input.Moving)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Idle());
    }

    public override void OnAnimationEvent() { }

    private void Rise()
    {
        Context.CurrentGravity += Context.JumpRiseGravity * Time.deltaTime;
    }
}