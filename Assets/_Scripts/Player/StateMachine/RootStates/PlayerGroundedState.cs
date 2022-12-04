public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        ResetGravity();
        Context.CoyoteTimer = Context.CoyoteTime;
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }
    
    public override void ShouldStateSwitch()
    {
        if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
        else switch (Context.Input.RollIsPressed)
        {
            case false when Context.JumpBufferTimer > 0.0f:
            case false when Context.Input.JumpWasPressed:
                SwitchState(Factory.Jump());
                break;
        }
    }

    protected override void ExitState() { }
    
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

    private void ResetGravity()
    {
        Context.CurrentGravity = PlayerStateMachine.GroundedGravity;
    }
}