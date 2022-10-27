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
        Context.CoyoteTimer = Context.CoyoteTime;
        Context.MovementVectorY = ResetGravity();
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT STATE: PlayerGroundedState");
        Context.Rigidbody.velocity = AdjustVectorToSlope(Context.Rigidbody.velocity);
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
        else
            SetSubState(Factory.Walk());
    }

    private Vector3 AdjustVectorToSlope(Vector3 movementVector)
    {
        var ray = new Ray(Context.PlayerPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedMovementVector = slopeRotation * movementVector;

            if (adjustedMovementVector.y < 0.0f)
                return adjustedMovementVector;
        }

        return movementVector;

        // Debug.Log(Context.GroundSlopeAngle);
        // if (Context.GroundSlopeAngle != 0.0f)
        // {

        // }
        //
        // if (Context.GroundSlopeAngle != 0.0f)
        // {
        //     Context.IsOnSlope = true;
        //     Context.SlopeAngleRotation = Quaternion.FromToRotation(Context.PlayerTransform.up, localGroundCheckHitNormal);
        //     Context.PlayerMovement = Context.SlopeAngleRotation * Context.PlayerMovement;
        //     
        //     Context.RelativeSlopeAngle = Vector3.Angle(Context.PlayerMovement, playerUp) - 90.0f;
        //     Context.GroundAngleRollable = Context.RelativeSlopeAngle > Context.MaxRollableSlopeAngle;
        // }
        // else
        // {
        //     Context.RelativeSlopeAngle = Vector3.Angle(Context.PlayerMovement, playerUp) - 90.0f;
        //     Context.GroundAngleRollable = true;
        //     Context.IsOnSlope = false;
        // }
    }

    private float ResetGravity()
    {
        Context.AppliedGravity = 0.0f;
        Context.CurrentGravity = Context.MinimumGravity;
        return Context.AppliedGravity;
    }
}
