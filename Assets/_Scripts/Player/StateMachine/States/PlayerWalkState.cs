public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

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
        if (Context.Input.RollIsPressed)
            SwitchState(Factory.Roll());
        else if (Context.Input.RunIsPressed)
            SwitchState(Factory.Run());
        else if (!Context.Input.MoveIsPressed)
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
}