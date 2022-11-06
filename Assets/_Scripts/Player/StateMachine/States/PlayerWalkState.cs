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
        Debug.LogWarning("CURRENT SUBSTATE: PlayerWalkState");
        Context.MovementVector = new Vector3(Context.MovementVector.x * Context.MovementSpeed, Context.MovementVector.y, Context.MovementVector.z * Context.MovementSpeed);
        // Context.MovementVectorX = Context.MovementDirection.x * Context.MovementSpeed;
        // Context.MovementVectorZ = Context.MovementDirection.z * Context.MovementSpeed;
        
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
        // else if (Context.Input.RollIsPressed)
        //     SwitchState(Factory.Roll());
    }

    public sealed override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}

























