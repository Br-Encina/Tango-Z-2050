public class PlayerAimIdleState : PlayerBaseState
{
    public PlayerAimIdleState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory) { }

    public override void EnterState() {

        

    }

    public override void UpdateState()
    {
        CheckSwichStates();
    }

    public override void ExitState() { }

    public override void CheckSwichStates()
    {
        if (Ctx.IsShootPressed && !Ctx.IsShooting)
        {
            SwitchState(Factory.Shoot());
        }
    }

    public override void InitializeSubState() { }
}
