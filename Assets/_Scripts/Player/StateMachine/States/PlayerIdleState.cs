public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() { }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Slashing)
            SwitchState(Factory.Slash());
        else if (Context.Input.RollIsPressed)
            SwitchState(Factory.Roll());
        else if (Context.Input.RunIsPressed)
            SwitchState(Factory.Run());
        else if (Context.Input.MoveIsPressed)
            SwitchState(Factory.Walk());
    }
    
    protected override void ExitState() { }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}