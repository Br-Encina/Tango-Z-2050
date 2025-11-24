using UnityEngine;

public class PlayerGrounededState : PlayerBaseState
{

    public PlayerGrounededState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
       InitializeSubState();
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
        if (_ctx.IsJumpPressed && !_ctx.RequireNewJumpPress)
        {
            SwitchState(_factory.Jump());
        }
    }
    public override void InitializeSubState()
    {
        if (_ctx.IsMovementPressed && !_ctx.IsRunPressed)
        {
            SetSubState(_factory.Walk());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsRunPressed)
        {
            SetSubState(_factory.Run());
        }
        else
        {
            SetSubState(_factory.Idle());
        }
    }
}
