public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() { }

    protected override void UpdateState()
    {
        Context.MovementVector *= Context.MovementSpeed;
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.RunIsPressed)
            SwitchState(Factory.Run());
        else if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
    }
    
    protected override void ExitState() { }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}