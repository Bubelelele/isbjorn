public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsRunning", true);
    }

    protected override void UpdateState()
    {
        var runSpeed = Context.MovementSpeed * Context.RunMultiplier;
        Context.MovementVector *= runSpeed;
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Slashing)
            SwitchState(Factory.Slash());
        else if (Context.Input.RollIsPressed)
            SwitchState(Factory.Roll());
        else if (!Context.Input.RunIsPressed)
            SwitchState(Factory.Walk());
        else if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsRunning", false);

    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void AnimationBehaviour()
    {
        throw new System.NotImplementedException();
    }
}