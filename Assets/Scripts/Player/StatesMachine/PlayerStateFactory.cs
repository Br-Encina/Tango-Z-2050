

using System.Collections.Generic;

enum PlayerStates
{
    Idle,
    Grounded,
    Jump,
    Run,
    Walk,
    Fall,
    Push,
    Aim,
    Aimidle,
    Shoot,
    Death
}

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        _states[PlayerStates.Idle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.Grounded] = new PlayerGrounededState(_context, this);
        _states[PlayerStates.Jump] = new PlayerJumpState(_context, this);
        _states[PlayerStates.Run] = new PlayerRunState(_context, this);
        _states[PlayerStates.Walk] = new PlayerWalkState(_context, this);
        _states[PlayerStates.Fall] = new PlayerFallState(_context, this);
        _states[PlayerStates.Push] = new PlayerPushState(_context, this);
        _states[PlayerStates.Aim] = new PlayerAimState(_context, this);
        _states[PlayerStates.Aimidle] = new PlayerAimIdleState(_context, this);
        _states[PlayerStates.Shoot] = new PlayerShootState(_context, this);
        _states[PlayerStates.Death] = new PlayerDeathState(_context, this);

    }

    public PlayerBaseState Idle()
    {
        return _states[PlayerStates.Idle];
    }
    public PlayerBaseState Grounded()
    {
        return _states[PlayerStates.Grounded];
    }
    public PlayerBaseState Jump()
    {
        return _states[PlayerStates.Jump];
    }
    public PlayerBaseState Run()
    {
        return _states[PlayerStates.Run];
    }
    public PlayerBaseState Walk()
    {
        return _states[PlayerStates.Walk];
    }
    public PlayerBaseState Fall()
    {
        return _states[PlayerStates.Fall];
    }
    public PlayerBaseState Push()
    {
        return _states[PlayerStates.Push];
    }
    public PlayerBaseState Aim()
    {
        return _states[PlayerStates.Aim];
    }
    public PlayerBaseState AimIdle()
    {
        return _states[PlayerStates.Aimidle];

    }
    public PlayerBaseState Shoot()
    {
        return _states[PlayerStates.Shoot];
    }
    public PlayerBaseState Death()
    {
        return _states[PlayerStates.Death];
    }
}
