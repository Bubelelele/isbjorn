using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        
    }

    protected override void UpdateState()
    {
        Context.PlayerMovementX *= Context.MovementMultiplier * Context.RunMultiplier;
        Context.PlayerMovementZ *= Context.MovementMultiplier * Context.RunMultiplier;
        if (Context.IsOnSlope && !Context.PlayerIsJumping)
            Context.PlayerMovementY *= Context.MovementMultiplier * Context.RunMultiplier;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.RollIsPressed && Context.GoingDownHill)
            SwitchState(Factory.Roll());
        else if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
        else if (Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SwitchState(Factory.Walk());
    }

    public override void InitialiseSubState()
    {
        throw new System.NotImplementedException();
    }
}
