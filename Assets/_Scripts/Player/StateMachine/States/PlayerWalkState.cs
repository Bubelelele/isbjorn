public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement) { }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsWalking", true);
    }

    protected override void UpdateState()
    {
        Context.MovementVector *= Context.MovementSpeed;
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
        else if (Context.Input.Running)
            SwitchState(Factory.Run());
        else if (!Context.Input.Moving)
            SwitchState(Factory.Idle());
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsWalking", false);
    }

    public override void InitializeSubState()
    {       
        throw new System.NotImplementedException();
    }

    public override void OnAnimationEvent() { }
}