using UnityEngine;

public class PlayerRoarState : PlayerBaseState
{
    private float _animationTimer;
    
    public PlayerRoarState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Debug.LogWarning("CURRENT STATE: " + Context.CurrentState);
        Context.Animator.SetTrigger("Roar");
        _animationTimer = Context.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void UpdateState()
    {
        _animationTimer -= Time.deltaTime;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Debug.Log("Roar has ended.");
    }

    public override void ShouldStateSwitch()
    {
        if (_animationTimer < 0)
            SwitchState(Factory.Grounded());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
