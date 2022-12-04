using UnityEngine;

public class PlayerSniffState : PlayerBaseState
{
    private float _animationTimer;
    private readonly int _sniff = Animator.StringToHash("Sniff");
    
    public PlayerSniffState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger(_sniff);
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

    public override void AnimationBehaviour()
    {
        Debug.LogWarning("sniff sniff");
    }
}
