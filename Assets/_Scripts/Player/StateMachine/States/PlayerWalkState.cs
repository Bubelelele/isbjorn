using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private readonly int _isWalking = Animator.StringToHash("IsWalking");

    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetBool(_isWalking, true);
    }

    protected override void UpdateState()
    {
        var walkSpeed = Context.MovementSpeed;
        Debug.LogWarning("CURRENT SUBSTATE: PlayerWalkState");
        Context.MovementVectorX = Context.MovementDirection.x * walkSpeed;
        Context.MovementVectorZ = Context.MovementDirection.z * walkSpeed;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool(_isWalking, false);
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

























