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
        var runSpeed = Context.MovementSpeed * Context.RunMultiplier;
        Debug.LogWarning("CURRENT SUBSTATE: PlayerRunState");
        Context.MovementVectorX *= runSpeed;
        Context.MovementVectorZ *= runSpeed;
        // Context.MovementVectorX = Context.MovementDirection.x * runSpeed;
        // Context.MovementVectorZ = Context.MovementDirection.z * runSpeed;
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
        // else if (Context.Input.RollIsPressed)
        //     SwitchState(Factory.Roll());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
