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
        Debug.LogWarning("CURRENT SUBSTATE: PlayerIdleState");
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        switch (Context.Input.MoveIsPressed)
        {
            case true when Context.Input.RunIsPressed:
                SwitchState(Factory.Run());
                break;
            case true:
                SwitchState(Factory.Walk());
                break;
        }
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
