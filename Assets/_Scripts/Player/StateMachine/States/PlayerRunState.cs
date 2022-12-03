public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() { }

    protected override void UpdateState()
    {
        var runSpeed = Context.MovementSpeed * Context.RunMultiplier;
        Context.MovementVector *= runSpeed;
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.Input.RunIsPressed)
            SwitchState(Factory.Walk());
        else if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
    }
    
    protected override void ExitState() { }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}