using UnityEngine;

public abstract class PlayerBaseState
{
    public PlayerBaseState CurrentSubState;
    public readonly bool LocksInput;
    
    protected PlayerStateMachine Context { get; }
    protected PlayerStateFactory Factory { get; }
    protected bool IsRootState { get; set; }
    protected bool RequiresAnimationEnd { get; set; }
    protected bool IsMomentumBased { get; set; }

    private PlayerBaseState _currentRootState;

    protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, bool locksInput)
    {
        Context = currentContext;
        Factory = playerStateFactory;
        LocksInput = locksInput;
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
        var currentRootState = Context.CurrentState;
        var currentSubState = currentRootState.CurrentSubState;

        switch (newState.IsMomentumBased)
        {
            case false when currentRootState.IsMomentumBased && newState.LocksInput:
            case true when !currentSubState.IsMomentumBased && currentSubState.LocksInput:
                return;
        }

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
        {
            _currentRootState?.SetSubState(newState);
        }
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
        // if (Context.CurrentState.CurrentSubState.IsMomentumBased && newSubState.LocksInput) return;
        CurrentSubState = newSubState;
        newSubState.SetRootState(this);
    }
}