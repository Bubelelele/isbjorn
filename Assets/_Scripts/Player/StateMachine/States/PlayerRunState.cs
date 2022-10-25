using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private readonly int _isRunning = Animator.StringToHash("IsRunning");
    
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetBool(_isRunning, true);
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
        Context.Animator.SetBool(_isRunning, false);
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.Input.MoveIsPressed)
            SwitchState(Factory.Idle());
        else if (!Context.Input.RunIsPressed)
            SwitchState(Factory.Walk());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
