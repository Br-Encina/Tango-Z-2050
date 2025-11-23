

public abstract class PlayerBaseState
{
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
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

    void UpdateStates()
    {
        UpdateState();
        CheckSwichStates();
    }

    void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();

        _ctx.CurrentState = newState;

    }

    void SetSuperState(PlayerBaseState newState)
    {
        // Implementation for setting the super state
    }

    void SetSubState(PlayerBaseState newState)
    {
        // Implementation for setting the sub state
    }

}
