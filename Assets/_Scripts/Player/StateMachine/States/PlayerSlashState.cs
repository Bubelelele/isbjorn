using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private readonly Collider[] _hitColliders = new Collider[5];

    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        // Context.RequiresInput = false;
        Context.Animator.SetTrigger("Slash");
    }

    protected override void UpdateState()
    {
        ShouldStateSwitch();
    }

    public override void ShouldStateSwitch()
    {
        switch (Context.AnimationEnded)
        {
            case true when Context.Input.RunIsPressed:
                SwitchState(Factory.Run());
                break;
            case true when Context.Input.MoveIsPressed:
                SwitchState(Factory.Walk());
                break;
            case true:
                SwitchState(Factory.Idle());
                break;
        }
    }
    
    protected override void ExitState()
    {
        // Context.RequiresInput = true;
        Context.AnimationEnded = false;
    }

    public override void InitializeSubState()
    {       
        throw new System.NotImplementedException();
    }

    public override void AnimationBehaviour()
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