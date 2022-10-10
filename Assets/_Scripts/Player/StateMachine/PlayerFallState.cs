using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitialiseSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Enter - " + Context.CurrentState);
    }

    protected override void UpdateState()
    {
        //Debug.Log(Context.CurrentState);
        HandleGravity();
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (Context.PlayerIsGrounded)
            SwitchState(Factory.Grounded());
    }

    public sealed override void InitialiseSubState()
    {
        if (!Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SetSubState(Factory.Idle());
        else if (Context.Input.MoveIsPressed && !Context.Input.RunIsPressed)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Run());
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
        Context.PlayerMovementY = Context.Gravity;
    }
}
