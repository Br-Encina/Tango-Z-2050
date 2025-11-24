using UnityEngine;

public class PlayerJumpState : PlayerBaseState, IRootState
{

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
       
    }
    public override void EnterState()
    {
        InitializeSubState();
        HandleJump();

    }
    public override void UpdateState()
    {
        HandleGravity();
        CheckSwichStates();
    }
    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        
        //if (Ctx.IsJumpPressed)
        //{
        //    Ctx.RequireNewJumpPress = true;
        //}
    }
    public override void CheckSwichStates()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {
        if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SetSubState(Factory.Run());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }

    void HandleJump()
    {
            Ctx.IsJumpPressed = false; 
            Ctx.IsJumping = true;
            Ctx.RequireNewJumpPress = true;

            Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);

            Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
            Ctx.ApliedMovementY = Ctx.InitialJumpVelocity;
        
       

    }

    public void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovementY <= 0f;
        float fallMultiplier = 2.0f;
        float previousY = Ctx.CurrentMovementY;

        if (isFalling)
        {

           Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * fallMultiplier * Time.deltaTime);
           Ctx.ApliedMovementY = Mathf.Max((previousY + Ctx.CurrentMovementY) * 0.5f, -20f);
        }
        else if(Ctx.CurrentMovementY > Ctx.Gravity)
        {
            Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
            Ctx.ApliedMovementY = (previousY + Ctx.CurrentMovementY) * 0.5f;
        }

        //if (isFalling)
        //{
        //    // Caída acelerada
        //    Ctx.CurrentMovementY += Ctx.Gravity * fallMultiplier * Time.deltaTime;
        //}
        //else
        //{
        //    // Subida normal
        //    Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
        //}

        //// Movimiento aplicado
        //Ctx.ApliedMovementY = (previousY + Ctx.CurrentMovementY) * 0.5f;

    }
}
