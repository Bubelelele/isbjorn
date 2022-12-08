using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        IsRootState = true;
        IsMomentumBased = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        var bounceVelocity = Mathf.Clamp(Context.InitialJumpVelocity - Context.CurrentGravity, Context.InitialJumpVelocity, -Context.MaximumGravity);
        Context.CurrentGravity = Context.LandedOnWalrus ? bounceVelocity : Context.InitialJumpVelocity;
        Context.JumpBufferTimer = 0.0f;
        Context.CoyoteTimer = 0.0f;
        Context.Input.JumpWasPressed = false;
        Context.Animator.SetTrigger("Jump");
        Context.Animator.SetBool("IsJumping", true);
        Context.JumpFeedback?.PlayFeedbacks();
        Context.AudioSources[8].Play();
    }

    protected override void UpdateState()
    {
        Rise();
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        switch (Context.CurrentGravity >= 0.0f)
        {
            case true when Context.Input.JumpIsHeld:
            case true when Context.LandedOnWalrus:
                return;
        }
        Context.CurrentGravity = 0.0f;
        Context.PlayerIsLandingJump = true;
        SwitchState(Factory.Fall());
    }

    protected override void ExitState()
    {
        Context.LandedOnWalrus = false;
        Context.Animator.SetBool("IsJumping", false);
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

    private void Rise()
    {
        Context.CurrentGravity += Context.JumpRiseGravity * Time.deltaTime;
    }
}