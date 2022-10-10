using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSniffState : PlayerBaseState
{
    private float _animationTimer;
    public PlayerSniffState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Sniff");
        _animationTimer = Context.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void UpdateState()
    {
        _animationTimer -= Time.deltaTime;
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        Debug.Log("Sniff has ended.");
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
