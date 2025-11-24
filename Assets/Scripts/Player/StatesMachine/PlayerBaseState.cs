

public abstract class PlayerBaseState
{
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
    protected PlayerBaseState _currentSuperState;
    protected PlayerBaseState _currentSubState;
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwichStates();
    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
        CheckSwichStates();
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();

        _ctx.CurrentState = newState;

    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        // Implementation for setting the super state
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        // Implementation for setting the sub state
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
