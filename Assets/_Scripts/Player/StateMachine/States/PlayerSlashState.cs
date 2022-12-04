using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private readonly Collider[] _hitColliders = new Collider[5];

    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Context.Animator.SetTrigger("Slash");
    }

    protected override void UpdateState()
    {
        Context.Immovable = true;
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        if (Context.AnimationEnded)
            SwitchState(Factory.Idle());
    }
    
    protected override void ExitState()
    {
        Context.Immovable = false;
        Context.AnimationEnded = false;
    }

    public override void InitializeSubState() { }

    public override void AnimationBehaviour()
    {
        Physics.OverlapSphereNonAlloc(PlayerStateMachine.staticPlayerTransform.position + new Vector3(0, 1.5f, 1.8f), 4f, _hitColliders);
        foreach (var hitCollider in _hitColliders)
        {
            if (hitCollider == null) break;
            if (hitCollider.TryGetComponent<IHittable>(out var hittable)) {
                hittable.Hit();
            }
        }
    }
}