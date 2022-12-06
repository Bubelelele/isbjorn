public class PlayerSniffState : PlayerBaseState
{
    public PlayerSniffState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        RequiresAnimationEnd = true;
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Sniff");
        Context.SniffFeedback?.PlayFeedbacks();
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    protected override void ExitState() { }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Running)
            SwitchState(Factory.Run());
        else if (Context.Input.Moving)
            SwitchState(Factory.Walk());
        else
            SwitchState(Factory.Idle());
    }

    public override void InitializeSubState() { }

    public override void OnAnimationEvent() { }
}
