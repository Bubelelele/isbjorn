using UnityEngine;

public class PlayerRoarState : PlayerBaseState
{
    private float _animationTimer;
    private readonly int _roar = Animator.StringToHash("Roar");

    public PlayerRoarState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger(_roar);
        _animationTimer = 2.2f;
        EffectSpawner.SpawnRoarFX(
            Context.modelTransform.position + Context.modelTransform.forward * 2.5f + Vector3.up * 1.5f, 
            Context.modelTransform.rotation);
    }

    protected override void UpdateState()
    {
        Context.MovementVector = Vector3.zero;
        _animationTimer -= Time.deltaTime;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (_animationTimer < 0.0f)
            SwitchState(Factory.Grounded());
    }

    public sealed override void InitializeSubState()
    {
        
    }
}
