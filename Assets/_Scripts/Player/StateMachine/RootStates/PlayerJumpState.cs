using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int _jumping = Animator.StringToHash("Jumping");
    private readonly int _isJumping = Animator.StringToHash("IsJumping");
    private bool _decent;

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        // Context.Animator.SetTrigger(_jumping);
        // Context.Animator.SetBool(_isJumping, true);
        Context.MovementVectorY = Context.JumpForce;
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT STATE: PlayerJumpState");
        Context.MovementVectorY = HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        // Context.Animator.SetBool(_isJumping, false);
        _decent = false;
    }

    public override void ShouldStateSwitch()
    {
        if (Context.PlayerIsGrounded)
            SwitchState(Factory.Grounded());
    }

    public sealed override void InitializeSubState()
    {
        if (!Context.Input.MoveIsPressed)
            SetSubState(Factory.Idle());
        else if (Context.Input.RunIsPressed)
            SetSubState(Factory.Run());
        else
            SetSubState(Factory.Walk());
    }
    
    private float HandleGravity()
    {
        Context.PlayerFallTimer -= Time.fixedDeltaTime;
        if (Context.PlayerFallTimer < 0.0f)
        {
            if (Context.CurrentGravity < 500.0f && !_decent)
            {
                Context.CurrentGravity -= Context.IncrementAmount;
            }
            else if (Context.CurrentGravity > Context.MaximumGravity && _decent)
            {
                Context.CurrentGravity += Context.IncrementAmount;
            }
            else
            {
                Context.CurrentGravity = Context.MinimumGravity;
                _decent = true;
            }
            Context.PlayerFallTimer = Context.IncrementFrequency;
            Context.Gravity = Context.CurrentGravity;
        }
        return Context.Gravity;
    }
}
