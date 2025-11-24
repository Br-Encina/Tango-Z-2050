using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory): base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, true);
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);
    }
    public override void UpdateState()
    {
        CheckSwichStates();
        _ctx.ApliedMovementZ = _ctx.CurrentMovementInput.x;
    }
    public override void ExitState()
    {

    }
    public override void CheckSwichStates()
    {
        if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsRunPressed)
        {
            SwitchState(_factory.Run());
        }
    }
    public override void InitializeSubState()
    {
    }
}
