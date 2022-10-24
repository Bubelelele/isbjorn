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
        Debug.LogWarning("CURRENT STATE: " + Context.CurrentState);
        
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
        else if (Context.Input.MoveIsPressed)
            SwitchState(Factory.Walk());
    }

    public sealed override void InitializeSubState()
    {
        
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
            Context.GroundAngleRollable = Context.RelativeSlopeAngle > Context.MaxRollableSlopeAngle;
        }
        else
        {
            Context.RelativeSlopeAngle = Vector3.Angle(Context.PlayerMovement, playerUp) - 90.0f;
            Context.GroundAngleRollable = true;
            Context.IsOnSlope = false;
        }
    }

    private void HandleGravity()
    {
        Context.PlayerMovementY = Context.Gravity;
    }
}
