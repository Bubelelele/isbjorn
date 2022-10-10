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
    }

    protected override void UpdateState()
    {
        Debug.Log(Context.GroundAngleRollable);
        Context.PlayerMovement = Vector3.zero;
        HandleGravity();
        HandleRollingMovement();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsRolling", false);
        Context.IsRolling = false;
        Debug.LogWarning("byebye");
    }

    public override void ShouldStateSwitch()
    {
        if (Context.PlayerIsGrounded && !Context.Input.RollIsPressed)
            SwitchState(Factory.Grounded());
        else if (!Context.PlayerIsGrounded && !Context.Input.RollIsPressed)
            SwitchState(Factory.Fall());
    }

    public override void InitialiseSubState()
    {
        throw new System.NotImplementedException();
    }

    private void HandleRollingMovement() {
        Debug.Log("big boner balls");
        Context.PlayerMovementZ = Context.RollMultiplier;
    }
    
    private void HandleGravity() {
        Context.PlayerMovementY = Context.Gravity;
    }
}
