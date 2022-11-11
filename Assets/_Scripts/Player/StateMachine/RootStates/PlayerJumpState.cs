using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int _isJumping = Animator.StringToHash("IsJumping");

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Entered jump state.");
        Context.Animator.SetBool(_isJumping, true);
        Context.CoyoteTimer = 0.0f;
        Context.CurrentGravity = Context.LandedOnWalrus ? Context.BounceVelocity : Context.InitialVelocity;
    }

    protected override void UpdateState()
    {
        // Debug.LogWarning("CURRENT STATE: PlayerJumpState");
        Context.MovementVectorY = HandleJump();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool(_isJumping, false);
        Context.LandedOnWalrus = false;
    }

    public override void ShouldStateSwitch()
    {
        if (Context.MovementVectorY < 0.0f)
        {
            Context.IsLandingJump = true;
            SwitchState(Factory.Fall());
        }
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
    
    private float HandleJump()
    {
        Context.PlayerInAirTimer -= Time.deltaTime;
        if (Context.PlayerInAirTimer < 0.0f)
        {
            Context.CurrentGravity -= Context.RiseDecrementAmount;
            Context.PlayerInAirTimer = Context.IncrementFrequency;
        }
        Context.AppliedGravity = Context.CurrentGravity;
        return Context.AppliedGravity;
    }
}
