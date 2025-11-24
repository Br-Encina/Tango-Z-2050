using UnityEngine;

public class PlayerPushState : PlayerBaseState
{
    public PlayerPushState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory) { }

    public override void EnterState()
    {
        // Activar animación de empuje
        Ctx.Animator.SetBool(Ctx.IsPushingHash, true);

        InitializeSubState();
    }

    public override void UpdateState()
    {
        CheckSwichStates();

        // Mover la caja
        if (Ctx.CurrentPushBox != null)
        {
            Vector3 pushDir = Ctx.transform.forward * Ctx.PushSpeed;
            Ctx.CurrentPushBox.Push(pushDir);
        }

        // Mover lento al player
        Ctx.ApliedMovementZ = Ctx.CurrentMovementInput.x * Ctx.PushSpeed;
    }

    public override void ExitState()
    {
        // Desactivar animación
        Ctx.Animator.SetBool(Ctx.IsPushingHash, false);
    }

    public override void CheckSwichStates()
    {
        if (!Ctx.IsPushPressed)
        {
            SwitchState(Factory.Grounded());
            return;
        }

        if (!Ctx.IsTouchingPushBox)
        {
            SwitchState(Factory.Grounded());
            return;
        }

        if (Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
            return;
        }

        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
            return;
        }
    }

    public override void InitializeSubState()
    {
        // No es necesario subestado en Push, pero lo dejamos por coherencia
        if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Idle());
    }
}
