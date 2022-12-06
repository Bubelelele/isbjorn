public abstract class PlayerBaseState
{
    public PlayerBaseState CurrentSubState;
    public readonly bool LocksMovement;
    
    protected PlayerStateMachine Context { get; }
    protected PlayerStateFactory Factory { get; }
    protected bool IsRootState { get; set; }
    protected bool RequiresAnimationEnd { get; set; }
    protected bool IsMomentumBased { get; set; }

    private PlayerBaseState _currentRootState;

    protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksMovement)
    {
        Context = currentContext;
        Factory = playerStateFactory;
        LocksMovement = locksMovement;
    }
    
    public abstract void EnterState();
    
    protected abstract void UpdateState();
    
    public void UpdateStates()
    {
        UpdateState();
        CurrentSubState?.UpdateStates();
    }
    
    public abstract void ShouldStateSwitch();
    
    protected void SwitchState(PlayerBaseState newState)
    {
        if (Context.CurrentState.IsMomentumBased && !newState.IsMomentumBased && newState.LocksMovement) return;

        if (RequiresAnimationEnd)
        {
            if (!Context.AnimationEnded) return;
            Switch(newState);
            Context.AnimationEnded = false;
        }
        else
            Switch(newState);
    }

    private void Switch(PlayerBaseState newState)
    {
        ExitState();
        
        newState.EnterState();

        if (IsRootState)
            Context.CurrentState = newState;
        else
            _currentRootState?.SetSubState(newState);
    }
    
    protected abstract void ExitState();

    public abstract void InitializeSubState();

    public abstract void OnAnimationEvent();
    
    private void SetRootState(PlayerBaseState newRootState)
    {
        _currentRootState = newRootState;
    }
    
    protected void SetSubState(PlayerBaseState newSubState)
    {
        CurrentSubState = newSubState;
        newSubState.SetRootState(this);
    }
}