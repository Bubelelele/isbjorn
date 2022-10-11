using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsWalking", true);
    }

    protected override void UpdateState()
    {
        Context.PlayerMovementX *= Context.MovementMultiplier;
        Context.PlayerMovementZ *= Context.MovementMultiplier;
        if (Context.IsOnSlope && !Context.PlayerIsJumping)
            Context.PlayerMovementY *= Context.MovementMultiplier;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsWalking", false);
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.RollIsPressed && Context.GroundAngleRollable)
            SwitchState(Factory.Roll());
        else if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
        else if (Context.Input.MoveIsPressed && Context.Input.RunIsPressed)
            SwitchState(Factory.Run());
    }

    public override void InitialiseSubState()
    {
        throw new System.NotImplementedException();
    }
}
