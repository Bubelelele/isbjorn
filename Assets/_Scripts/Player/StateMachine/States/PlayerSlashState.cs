using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private readonly Collider[] _hitColliders = new Collider[5];

    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        RequiresAnimationEnd = true;
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Slash");
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.Input.Running)
            SwitchState(Factory.Run());
        else if (Context.Input.Moving)
            SwitchState(Factory.Walk());
        else
            SwitchState(Factory.Idle());
    }
    
    protected override void ExitState() { }

    public override void InitializeSubState() { }

    public override void OnAnimationEvent()
    {
        Physics.OverlapSphereNonAlloc(PlayerStateMachine.StaticPlayerTransform.position + new Vector3(0, 1.5f, 1.8f), 4f, _hitColliders);
        foreach (var hitCollider in _hitColliders)
        {
            if (hitCollider == null) break;
            if (hitCollider.TryGetComponent<IHittable>(out var hittable)) {
                hittable.Hit();
            }
        }
    }
}