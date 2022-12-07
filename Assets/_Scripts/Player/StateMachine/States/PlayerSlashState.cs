using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private readonly Collider[] _hitColliders = new Collider[5];

    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement) : base(currentContext, playerStateFactory, locksMovement)
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