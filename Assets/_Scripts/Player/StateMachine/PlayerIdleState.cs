using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        
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
        if (Context.Input.RollIsPressed && Context.GroundAngleRollable)
            SwitchState(Factory.Roll());
        else if (Context.Input.MoveIsPressed && Context.Input.RunIsPressed)
            SwitchState(Factory.Run());
        else if (Context.Input.MoveIsPressed)
            SwitchState(Factory.Walk());
    }

    public override void InitialiseSubState()
    {
        throw new System.NotImplementedException();
    }
}
