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
        Debug.LogWarning("CURRENT STATE: " + Context.CurrentState);
        
        Context.Rigidbody.MovePosition(Context.Rigidbody.position + MoveDirection() * Time.fixedDeltaTime);
        // Context.PlayerMovementX *= Context.MovementMultiplier;
        // Context.PlayerMovementZ *= Context.MovementMultiplier;
        // if (Context.IsOnSlope && !Context.PlayerIsJumping)
        //     Context.PlayerMovementY *= Context.MovementMultiplier;
        //
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsWalking", false);
    }

    public override void ShouldStateSwitch()
    {
        // if (Context.Input.RollIsPressed && Context.GroundAngleRollable)
        //     SwitchState(Factory.Roll());
        // else if (!Context.Input.MoveIsPressed)
        //     SwitchState(Factory.Idle());
        // else if (Context.Input.MoveIsPressed && Context.Input.RunIsPressed)
        //     SwitchState(Factory.Run());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    
    private Vector3 MoveDirection()
    {
        return new Vector3(Context.Input.MoveInput.x, 0.0f, Context.Input.MoveInput.y);
    }
}
