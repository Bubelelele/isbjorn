public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement) { }

    public override void EnterState() { }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Sniffing)
            SwitchState(Factory.Sniff());
        else if (Context.Input.Roaring)
            SwitchState(Factory.Roar());
        else if (Context.Input.Slashing)
            SwitchState(Factory.Slash());
        else if (Context.Input.Rolling)
            SwitchState(Factory.Roll());
        else switch (Context.Input.Moving)
        {
            case true when Context.Input.Running:
                SwitchState(Factory.Run());
                break;
            case true:
                SwitchState(Factory.Walk());
                break;
        }
    }
    
    protected override void ExitState() { }

    public override void InitializeSubState() 
    {       
        throw new System.NotImplementedException();
    }

    public override void OnAnimationEvent() { }
}