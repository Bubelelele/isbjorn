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
        // Debug.LogWarning("CURRENT SUBSTATE: PlayerIdleState");
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.RunIsPressed && Context.Input.MoveIsPressed)
            SwitchState(Factory.Run());
        else if (Context.Input.MoveIsPressed)
            SwitchState(Factory.Walk());
        else if (Context.Input.RollIsPressed)
            SwitchState(Factory.Roll());
        else if (Context.Input.Slashing)
            SwitchState(Factory.Slash());
        else if (Context.Input.Roaring)
            SwitchState(Factory.Roar());
        else if (Context.Input.Sniffing)
            SwitchState(Factory.Sniff());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
