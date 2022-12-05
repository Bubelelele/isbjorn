public class PlayerSniffState : PlayerBaseState
{
    public PlayerSniffState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
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

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void AnimationBehaviour() { }
}
