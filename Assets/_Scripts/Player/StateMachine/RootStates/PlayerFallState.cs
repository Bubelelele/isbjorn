using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() { }
    
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
        if (!Context.Input.RollIsPressed && Context.CoyoteTimer > 0.0f && Context.Input.JumpWasPressed)
            SwitchState(Factory.Jump());
        else if (Context.PlayerIsGrounded) {
            SwitchState(Factory.Grounded());
            Context.LandingFeedback?.PlayFeedbacks();
        }
    }
    
    protected override void ExitState()
    {
        Context.PlayerIsLandingJump = false;
    }

    public sealed override void InitializeSubState()
    {
        if (Context.Input.RollIsPressed)
            SetSubState(Factory.Roll());
        else if (Context.Input.RunIsPressed)
            SetSubState(Factory.Run());
        else if (Context.Input.MoveIsPressed)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Idle());
    }
    
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