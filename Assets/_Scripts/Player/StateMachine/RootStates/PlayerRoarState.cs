public class PlayerRoarState : PlayerBaseState
{
    public PlayerRoarState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

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
        
    }

    public sealed override void InitializeSubState() { }

    public override void AnimationBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
