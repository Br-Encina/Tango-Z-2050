using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, false);
        _ctx.Animator.SetBool(_ctx.IsRunningHash, true);

    }
    public override void UpdateState()
    {
        CheckSwichStates();
        _ctx.ApliedMovementZ = _ctx.CurrentMovementInput.x * _ctx.RunMultipler;
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
        else if (_ctx.IsMovementPressed && !_ctx.IsRunPressed)
        {
            SwitchState(_factory.Walk());
        }
    }
    public override void InitializeSubState()
    {
    }

}
