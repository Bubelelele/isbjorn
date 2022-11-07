using UnityEngine;

public class PlayerRoarState : PlayerBaseState
{
    private float _animationTimer;
    private readonly int _roar = Animator.StringToHash("Roar");

    public PlayerRoarState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger(_roar);
        _animationTimer = Context.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void UpdateState()
    {
        _animationTimer -= Time.deltaTime;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        if (_animationTimer < 0.0f)
            SwitchState(Factory.Idle());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
