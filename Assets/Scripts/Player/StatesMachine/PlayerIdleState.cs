using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, false);
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);
        _ctx.ApliedMovementZ = 0f;

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
        if (_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Walk());
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
