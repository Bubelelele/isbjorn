using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetBool("IsRolling", true);
        Context.IsRolling = true;
        Context.Rigidbody.drag = Context.Drag * 0.5f;
    }

    protected override void UpdateState()
    {
        Debug.Log(Context.GoingDownHill);
        HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsRolling", false);
        Context.IsRolling = false;
        Context.Rigidbody.drag = Context.Drag;
    }

    public override void ShouldStateSwitch()
    {
        if (Context.PlayerIsGrounded && !Context.Input.RollIsPressed)
            SwitchState(Factory.Grounded());
        else if (!Context.PlayerIsGrounded)
            SwitchState(Factory.Fall());
    }

    public override void InitialiseSubState()
    {
        throw new System.NotImplementedException();
    }
    
    private void HandleGravity()
    {
        Context.PlayerFallTimer -= Time.fixedDeltaTime;
        if (Context.PlayerFallTimer < 0.0f)
        {
            if (Context.Gravity > Context.MaximumGravity)
            {
                Context.Gravity += Context.IncrementAmount;
            }
            Context.PlayerFallTimer = Context.IncrementFrequency;
        }

        if (Context.GroundSlopeAngle == 0.0f)
            Context.PlayerMovement = Context.GroundSlopeAngle * Context.PlayerMovement;
        else
            Context.PlayerMovementY = Context.GroundSlopeAngle * Context.Gravity;
    }
}
