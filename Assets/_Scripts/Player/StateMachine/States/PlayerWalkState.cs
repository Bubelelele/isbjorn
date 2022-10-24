using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT SUBSTATE: PlayerWalkState");
        Context.MovementVectorX = Context.MovementDirection.x * Context.MovementSpeed;
        Context.MovementVectorZ = Context.MovementDirection.z * Context.MovementSpeed;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
        else if (Context.Input.RunIsPressed)
            SwitchState(Factory.Run());
    }

    public sealed override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
