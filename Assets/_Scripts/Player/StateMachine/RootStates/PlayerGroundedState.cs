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
        
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT STATE: PlayerGroundedState");
        Context.MovementVectorY = HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
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

    private void HandleSlopes()
    {
        // var playerUp = Context.PlayerTransform.up;
        // var localGroundCheckHitNormal = Context.PlayerTransform.InverseTransformDirection(Context.GroundCheckHit.normal);
        // Context.GroundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, playerUp);
        // Debug.Log(Context.GroundSlopeAngle);
        // if (Context.GroundSlopeAngle != 0.0f)
        // {
        //     Context.SlopeAngleRotation = Quaternion.FromToRotation(Context.PlayerTransform.up, localGroundCheckHitNormal);
        //     Context.MovementDirection = Context.SlopeAngleRotation * Context.MovementDirection;
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

    private float HandleGravity()
    {
        Context.Gravity = 0.0f;
        Context.CurrentGravity = Context.MinimumGravity;
        return Context.Gravity;
    }
}
