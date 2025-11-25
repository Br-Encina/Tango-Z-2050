using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.ApliedMovementZ = 0f;

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
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }

    }
    public override void InitializeSubState()
    {
    }
}
