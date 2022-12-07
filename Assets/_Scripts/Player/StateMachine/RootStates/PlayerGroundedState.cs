using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        ResetGravity();
        Context.CoyoteTimer = Context.CoyoteTime;
        Context.AudioSources[7].Play();
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }
    
    public override void ShouldStateSwitch()
    {
        if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
        else if (Context.JumpBufferTimer > 0.0f || Context.Input.JumpWasPressed)
            SwitchState(Factory.Jump());
    }

    protected override void ExitState() { }
    
    public sealed override void InitializeSubState()
    {
        if (Context.Input.Rolling)
            SetSubState(Factory.Roll());
        else if (Context.Input.Running)
            SetSubState(Factory.Run());
        else if (Context.Input.Moving)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Idle());
    }

    public override void OnAnimationEvent() { }

    private void ResetGravity()
    {
        Context.CurrentGravity = PlayerStateMachine.GroundedGravity;
    }
}