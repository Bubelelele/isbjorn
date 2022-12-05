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
        switch (Context.AnimationEnded)
        {
            case true when Context.Input.RunIsPressed:
                SwitchState(Factory.Run());
                break;
            case true when Context.Input.MoveIsPressed:
                SwitchState(Factory.Walk());
                break;
            case true:
                SwitchState(Factory.Idle());
                break;
        }
    }

    public sealed override void InitializeSubState() { }

    public override void AnimationBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
