public abstract class PlayerBaseState
{
    protected bool IsRootState { get; set; }
    protected PlayerStateMachine Context { get; }
    protected PlayerStateFactory Factory { get; }
    
    private PlayerBaseState _currentRootState;
    public PlayerBaseState currentSubState;

    protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        Context = currentContext;
        Factory = playerStateFactory;
    }
    
    public abstract void EnterState();
    
    protected abstract void UpdateState();
    
    public void UpdateStates()
    {
        UpdateState();
        currentSubState?.UpdateStates();
    }
    
    public abstract void ShouldStateSwitch();
    
    protected void SwitchState(PlayerBaseState newState)
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

    public abstract void AnimationBehaviour();
    
    private void SetRootState(PlayerBaseState newRootState)
    {
        _currentRootState = newRootState;
    }
    
    protected void SetSubState(PlayerBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetRootState(this);
    }
}