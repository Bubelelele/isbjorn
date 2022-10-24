using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Debug.LogWarning("CURRENT STATE: " + Context.CurrentState);
    }

    protected override void UpdateState()
    {
        
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
