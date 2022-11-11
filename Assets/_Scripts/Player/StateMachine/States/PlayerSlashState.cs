using UnityEngine;

public class PlayerSlashState : PlayerBaseState
{
    private float _animationTimer;
    private readonly int _slash = Animator.StringToHash("Slash");
    private readonly Collider[] _hitColliders = new Collider[5];

    public PlayerSlashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Context.Animator.SetTrigger(_slash);
        _animationTimer = Context.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void UpdateState()
    {
        // Debug.LogWarning("CURRENT STATE: PlayerSlashState");
        // Debug.Log(_animationTimer);
        _animationTimer -= Time.deltaTime;
        if (_animationTimer < .2f) {
            Physics.OverlapSphereNonAlloc(Context.PlayerTransform.position + new Vector3(0, 1.5f, 1.8f), 4f, _hitColliders);
            for (var i = 0; i < _hitColliders.Length; i++) {
                var hitCollider = _hitColliders[i];
                if (hitCollider == null) return;
                if (hitCollider.TryGetComponent<IHittable>(out var hittable)) {
                    hittable.Hit();
                }
            }
        }
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
}