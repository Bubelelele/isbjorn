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

    protected override void ExitState()
    {
        Context.ShowPath.myLineRenderer.SetActive(false);
    }

    public override void ShouldStateSwitch()
    {
        switch (Context.Input.Moving)
        {
            case true when Context.Input.Running:
                SwitchState(Factory.Run());
                break;
            case true:
                SwitchState(Factory.Walk());
                break;
            default:
                SwitchState(Factory.Idle());
                break;
        }
    }

    public override void InitializeSubState() { }

    public override void OnAnimationEvent()
    {
        Context.ShowPath.FindFood();
    }
}
