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
        Context.CurrentGravity = Context.InitialJumpVelocity;
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
        Context.Animator.SetBool("IsJumping", false);
    }
    
    public sealed override void InitializeSubState()
    {
        if (Context.Input.RollIsPressed)
            SwitchState(Factory.Roll());
        else if (Context.Input.RunIsPressed)
            SetSubState(Factory.Run());
        else if (Context.Input.MoveIsPressed)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Idle());
    }

    public override void AnimationBehaviour() { }

    private void Rise()
    {
        Context.CurrentGravity += Context.JumpRiseGravity * Time.deltaTime;
    }
}