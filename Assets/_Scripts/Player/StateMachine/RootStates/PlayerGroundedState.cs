using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Entered grounded state.");
        Context.MovementVectorY = ResetGravity();
        Context.BounceVelocity = 0.0f;
        Context.CoyoteTimer = Context.CoyoteTime;
    }

    protected override void UpdateState()
    {
        // Debug.LogWarning("CURRENT STATE: PlayerGroundedState");
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
        else if (Context.Input.JumpIsPressed)
            SwitchState(Factory.Jump());
    }

    public sealed override void InitializeSubState()
    {
        if (!Context.Input.MoveIsPressed)
            SetSubState(Factory.Idle());
        else if (Context.Input.RunIsPressed)
            SetSubState(Factory.Run());
        // else if (Context.Input.Slashing)
        //     SwitchState(Factory.Slash());
        else
            SetSubState(Factory.Walk());
    }

    private float ResetGravity()
    {
        Context.AppliedGravity = 0.0f;
        Context.CurrentGravity = Context.MinimumGravity;
        return Context.AppliedGravity;
    }
}
