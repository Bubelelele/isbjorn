using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitialiseSubState();
    }
    
    public override void EnterState()
    {
        Context.Gravity = Context.GroundedGravity;
        Context.PlayerIsJumping = false;
        //Debug.Log("Enter - " + Context.CurrentState);
    }

    protected override void UpdateState()
    {
        //Debug.Log(Context.CurrentState);
        HandleGravity();
        HandleSlopes();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.PlayerIsJumping && Context.Input.JumpIsPressed)
            SwitchState(Factory.Jump());
        else if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
    }

    public sealed override void InitialiseSubState()
    {
        if (Context.Input.RollIsPressed && Context.GoingDownHill)
            SetSubState(Factory.Roll());
        else if (Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SetSubState(Factory.Walk());
        else if (!Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SetSubState(Factory.Idle());
        else
            SetSubState(Factory.Run());
    }

    private void HandleSlopes()
    {
        var playerUp = Context.PlayerTransform.up;
        var localGroundCheckHitNormal = Context.PlayerTransform.InverseTransformDirection(Context.GroundCheckHit.normal);
        Context.GroundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, playerUp);

        if (Context.GroundSlopeAngle != 0.0f)
        {
            Context.IsOnSlope = true;
            Context.SlopeAngleRotation = Quaternion.FromToRotation(Context.PlayerTransform.up, localGroundCheckHitNormal);
            Context.PlayerMovement = Context.SlopeAngleRotation * Context.PlayerMovement;
            
            Context.RelativeSlopeAngle = Vector3.Angle(Context.PlayerMovement, playerUp) - 90.0f;
            Context.GoingDownHill = Context.RelativeSlopeAngle > 0.0f;
        }
        else
        {
            Context.RelativeSlopeAngle = Vector3.Angle(Context.PlayerMovement, playerUp) - 90.0f;
            Context.GoingDownHill = Context.RelativeSlopeAngle > 0.0f;
            Context.IsOnSlope = false;
        }
    }

    private void HandleGravity()
    {
        Context.PlayerMovementY = Context.Gravity;
    }
}
