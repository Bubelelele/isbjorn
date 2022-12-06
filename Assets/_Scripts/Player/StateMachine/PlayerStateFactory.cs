public class PlayerStateFactory
{
    private readonly PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }
    
    public PlayerBaseState Grounded() { return new PlayerGroundedState(_context, this, false); }
    public PlayerBaseState Fall() { return new PlayerFallState(_context, this, false); }
    public PlayerBaseState Jump() { return new PlayerJumpState(_context, this, false); }
    public PlayerBaseState Idle() { return new PlayerIdleState(_context, this, false); }
    public PlayerBaseState Walk() { return new PlayerWalkState(_context, this, false); }
    public PlayerBaseState Run() { return new PlayerRunState(_context, this, false); }
    public PlayerBaseState Roll() { return new PlayerRollState(_context, this, true); }
    public PlayerBaseState Roar() { return new PlayerRoarState(_context, this, true); }
    public PlayerBaseState Slash() { return new PlayerSlashState(_context, this, true); }
    public PlayerBaseState Sniff() { return new PlayerSniffState(_context, this, true); }
}