using UnityEngine;

public class PlayerPushState : PlayerBaseState, IRootState
{
    public PlayerPushState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory) { 
     
        IsRootState = true;
    }

    public void HandleGravity()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            Ctx.ApliedMovementY = Ctx.Gravity; // Small downward force to keep grounded
        }
        else
        {
            Ctx.ApliedMovementY += Ctx.Gravity * Time.deltaTime;
        }
    }

    public override void EnterState()
    {

        
        Ctx.Animator.SetBool(Ctx.IsPushingHash, true);
        HandleGravity();




    }

    public override void UpdateState()
    {
        if (!Ctx.IsMovementPressed)
            Ctx.ApliedMovementZ = 0;

        HandlePush();
        CheckSwichStates();
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
        if (Ctx.IsDead)
        {
            SwitchState(Factory.Death());
            return;
        }






    }

    public override void InitializeSubState()
    {
        
    }

    void HandlePush()
    {
        
        // Mover la caja
        if (Ctx.CurrentPushBox != null)
        {
            Vector3 pushDir = Ctx.transform.forward * Ctx.PushSpeed;
            Ctx.CurrentPushBox.Push(pushDir);
        }

        // Mover lento al player
        Ctx.ApliedMovementZ = Ctx.CurrentMovementInput.x * Ctx.PushSpeed;
    }
}
