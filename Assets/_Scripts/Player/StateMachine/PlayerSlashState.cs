using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private float _animationTimer;
    
    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Slash");
        _animationTimer = Context.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void UpdateState()
    {
        _animationTimer -= Time.deltaTime;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Debug.Log("Slash has ended.");
    }

    public override void ShouldStateSwitch()
    {
        if (_animationTimer < 0)
            SwitchState(Factory.Grounded());
    }

    public override void InitialiseSubState()
    {
        throw new System.NotImplementedException();
    }
}