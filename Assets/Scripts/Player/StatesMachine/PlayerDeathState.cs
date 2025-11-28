using UnityEngine;


public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("PLAYER DEAD");

        Ctx.IsDead = true;

        // Detener movimiento
        Ctx.ApliedMovementZ = 0;
        Ctx.ApliedMovementY = 0;

        // Activar animación
        Ctx.Animator.SetBool(Ctx.IsDeadHash, true);

        // Mostrar UI
        DeathUIManager ui = GameObject.FindAnyObjectByType<DeathUIManager>();
        ui.ShowDeathScreen();
    }

    public override void UpdateState()
    {
        // No hacemos nada, el juego está pausado
    }

    public override void ExitState()
    {
        // Por si necesitás revivir
        Ctx.Animator.SetBool(Ctx.IsDeadHash, false);
    }

    public override void CheckSwichStates()
    {
        // No se puede salir de este estado
    }

    public override void InitializeSubState() { }
}
