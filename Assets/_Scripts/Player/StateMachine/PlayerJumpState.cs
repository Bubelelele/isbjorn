using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitialiseSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Enter" + Context.CurrentState);
        PlayerJump();
    }

    protected override void UpdateState()
    {
        //Debug.Log(Context.CurrentState);
        HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.Input.JumpIsPressed && Context.PlayerIsJumping && Context.PlayerIsGrounded)
            SwitchState(Factory.Grounded());
        else if (Context.PlayerMovementY <= 0.0f)
            SwitchState(Factory.Fall());
    }

    public sealed override void InitialiseSubState()
    {
        if (!Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SetSubState(Factory.Idle());
        else if (Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Run());
    }

    private void PlayerJump()
    {
        Context.PlayerIsJumping = true;
        Context.PlayerMovementY = Context.InitialJumpForce;
    }

    private void HandleGravity()
    {
        Context.PlayerMovementY = Context.Gravity;
    }
}
