using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    // private float _lastFrameAngle;
    // private bool _groundedLastFrame;
    // private float _eulerAngleVelocity = 10f;

    public override void EnterState()
    {
        // Debug.LogWarning("CURRENT STATE: " + Context.CurrentState);
        // Context.Animator.SetBool("IsRolling", true);
        // Context.IsRolling = true;
    }

    protected override void UpdateState()
    {
        // Debug.Log(Context.GroundAngleRollable);
        // RotateBear();
        // HandleRollingMovement();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        // Context.Animator.SetBool("IsRolling", false);
        // Context.IsRolling = false;
        // Debug.LogWarning("byebye");
    }

    public override void ShouldStateSwitch()
    {
        // if (Context.PlayerIsGrounded && !Context.Input.RollIsPressed)
        //     SwitchState(Factory.Grounded());
        // else if (!Context.PlayerIsGrounded && !Context.Input.RollIsPressed)
        //     SwitchState(Factory.Fall());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    // private void HandleRollingMovement() {
    //     
    //     Context.PlayerMovement = Vector3.zero;
    //     
    //     Context.PlayerMovementZ = Context.RollMultiplier;
    //     if (Context.RelativeSlopeAngle < 0f) Context.PlayerMovementZ = Context.RollMultiplier * 5;
    //     
    //     Context.PlayerMovementY = -10f;
    //     if (!Context.PlayerIsGrounded) {
    //         if (_groundedLastFrame && _lastFrameAngle < 0f) {
    //             Context.PlayerMovement = Vector3.zero;
    //             Context.PlayerMovementY = 100f;
    //             return;
    //         } else {
    //             Context.PlayerMovementY = -1000f;
    //         }
    //
    //         _groundedLastFrame = false;
    //     } else {
    //         _groundedLastFrame = true;
    //     }
    //     
    //     _lastFrameAngle = Context.RelativeSlopeAngle;
    //
    //     Context.PlayerMovement = Context.SlopeAngleRotation * Context.PlayerMovement;
    // }
    //
    // private void RotateBear()
    // {
    //     Context.PlayerTransform.GetChild(0).Rotate(Vector3.right * _eulerAngleVelocity, Space.Self);
    // }
}
