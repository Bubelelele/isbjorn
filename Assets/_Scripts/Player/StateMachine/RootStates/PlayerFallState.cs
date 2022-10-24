using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private float _fallTimer;
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        
    }

    protected override void UpdateState()
    {
        Debug.LogWarning("CURRENT STATE: PlayerFallState");
        Context.MovementVectorY = HandleGravity();
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
            if (Context.CurrentGravity > Context.MaximumGravity)
            {
                Context.CurrentGravity += Context.IncrementAmount;
            }
            Context.PlayerFallTimer = Context.IncrementFrequency;
            Context.Gravity = Context.CurrentGravity;
        }
        return Context.Gravity;
    }
}
