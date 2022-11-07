using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private float _animationTimer;
    private readonly int _slash = Animator.StringToHash("Slash");

    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger(_slash);
        _animationTimer = Context.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void UpdateState()
    {
        // Debug.LogWarning("CURRENT STATE: PlayerSlashState");
        // Debug.Log(_animationTimer);
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