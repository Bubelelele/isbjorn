using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement) { }

    private const float BreatheTime = 3.0f;
    private float _runningTimer = 10.0f;

    public override void EnterState()
    {
        Context.Animator.SetBool("IsRunning", true);
    }

    protected override void UpdateState()
    {
        var runSpeed = Context.MovementSpeed * Context.RunMultiplier;
        Context.MovementVector *= runSpeed;
        if (_runningTimer > 0.0f)
            _runningTimer -= Time.deltaTime;
        else
        {
            Context.AudioSources[4].Play();
            _runningTimer = BreatheTime;
        }
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Sniffing)
            SwitchState(Factory.Sniff());
        else if (Context.Input.Roaring)
            SwitchState(Factory.Roar());
        else if (Context.Input.Slashing)
            SwitchState(Factory.Slash());
        else if (Context.Input.Rolling)
            SwitchState(Factory.Roll());
        else if (!Context.Input.Running)
            SwitchState(Factory.Walk());
        else if (!Context.Input.Moving)
            SwitchState(Factory.Idle());
    }

    protected override void ExitState()
    {
        Context.Animator.SetBool("IsRunning", false);
    }

    public override void InitializeSubState()
    {       
        throw new System.NotImplementedException();
    }

    public override void OnAnimationEvent() { }
}