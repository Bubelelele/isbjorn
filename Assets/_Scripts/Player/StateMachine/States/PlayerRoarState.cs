public class PlayerRoarState : PlayerBaseState
{
    public PlayerRoarState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        RequiresAnimationEnd = true;
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Roar");
        Context.RoarFeedback?.PlayFeedbacks();
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Running)
            SwitchState(Factory.Run());
        else if (Context.Input.Moving)
            SwitchState(Factory.Walk());
        else
            SwitchState(Factory.Idle());
    }

    public sealed override void InitializeSubState() { }

    public override void OnAnimationEvent() { }
}
