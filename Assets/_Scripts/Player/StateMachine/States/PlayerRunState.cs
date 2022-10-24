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
        Debug.LogWarning("CURRENT SUBSTATE: PlayerRunState");
        Context.MovementVectorX = Context.MovementDirection.x * Context.MovementSpeed * Context.RunMultiplier;
        Context.MovementVectorZ = Context.MovementDirection.z * Context.MovementSpeed * Context.RunMultiplier;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.Input.RunIsPressed)
            SwitchState(Factory.Walk());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
