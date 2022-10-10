public abstract class PlayerBaseState
{
    protected bool IsRootState { get; set; }
    protected PlayerStateMachine Context { get; }
    protected PlayerStateFactory Factory { get; }
    
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;

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
        _currentSubState?.UpdateStates();
    }
    
    protected abstract void ExitState();
    public abstract void ShouldStateSwitch();
    
    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        
        newState.EnterState();

        if (IsRootState)
            Context.CurrentState = newState;
        else
            _currentSuperState?.SetSubState(newState);
    }
    
    public abstract void InitialiseSubState();
    
    private void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    
    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
