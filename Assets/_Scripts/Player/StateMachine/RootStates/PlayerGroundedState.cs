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
        if (!Context.PlayerIsGrounded)
            Context.CoyoteTimer -= Time.deltaTime;
        ShouldStateSwitch();
    }
    
    public override void ShouldStateSwitch()
    {
        if (Context.JumpBufferTimer > 0.0f)
            SwitchState(Factory.Jump());
        else switch (Context.CoyoteTimer)
        {
            case > 0.0f when Context.Input.JumpWasPressed:
                SwitchState(Factory.Jump());
                break;
            case < 0.0f:
                SwitchState(Factory.Fall());
                break;
            default:
            {
                if (Context.Input.JumpWasPressed)
                    SwitchState(Factory.Jump());
                break;
            }
        }
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