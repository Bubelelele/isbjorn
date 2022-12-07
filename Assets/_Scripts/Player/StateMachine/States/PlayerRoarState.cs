using UnityEngine;

public class PlayerRoarState : PlayerBaseState
{
    public PlayerRoarState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
    {
        RequiresAnimationEnd = true;
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Roar");
        Context.RoarFeedback?.PlayFeedbacks();
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    protected override void ExitState()
    {
        
    }

    public override void ShouldStateSwitch()
    {
        switch (Context.Input.Moving)
        {
            case true when Context.Input.Running:
                SwitchState(Factory.Run());
                break;
            case true:
                SwitchState(Factory.Walk());
                break;
            default:
                SwitchState(Factory.Idle());
                break;
        }
    }

    public sealed override void InitializeSubState() { }

    public override void OnAnimationEvent() { }
}
