using UnityEngine;

public class PlayerGrounededState : PlayerBaseState
{

    public PlayerGrounededState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
       
    }
    public override void EnterState()
    {
        _ctx.CurrentMovementY = _ctx.GroundedGravity;
        _ctx.ApliedMovementY = _ctx.GroundedGravity;
    }
    public override void UpdateState()
    {
        CheckSwichStates();
    }
    public override void ExitState()
    {

    }
    public override void CheckSwichStates()
    {
        if (_ctx.IsJumpPressed)
        {
            SwitchState(_factory.Jump());
        }
    }
    public override void InitializeSubState()
    {

    }
}
