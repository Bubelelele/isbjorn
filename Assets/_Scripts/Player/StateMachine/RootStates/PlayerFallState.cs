using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private float _fallTimer;
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    private float _gravityIncreaseTimer = 0.0f;
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
        if (Context.PlayerIsGrounded)
            SwitchState(Factory.Grounded());
    }

    public sealed override void InitializeSubState()
    {
        
    }

    private void HandleGravity()
    {
        // Context.PlayerFallTimer -= Time.fixedDeltaTime;
        // if (Context.PlayerFallTimer < 0.0f)
        // {
        //     if (Context.Gravity > Context.MaximumGravity)
        //     {
        //         Context.Gravity += Context.IncrementAmount;
        //     }
        //     Context.PlayerFallTimer = Context.IncrementFrequency;
        // }
        
        // _gravityIncreaseTimer += Time.deltaTime;
        // if (_gravityIncreaseTimer > 1.0f)
        // {
        //     Context.Gravity += Context.Gravity;
        //     _gravityIncreaseTimer = 0.0f;
        // }
        Context.Rigidbody.MovePosition(Context.Rigidbody.position + Vector3.down * Context.Gravity * Time.fixedDeltaTime);
    }
}
