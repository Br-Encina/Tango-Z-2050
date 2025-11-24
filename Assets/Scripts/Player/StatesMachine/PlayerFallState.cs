
using UnityEngine;
public class PlayerFallState : PlayerBaseState, IRootState
{
    float groundedConfirmTimer = 0f;
    const float groundedConfirmThreshold = 0.03f;
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        Ctx.CurrentMovementY = Ctx.ApliedMovementY;
    }
    public override void ExitState() { }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwichStates();


    }

    public override void CheckSwichStates()
    {
        //if (Ctx.CharacterController.isGrounded)
        //{
        //    SwitchState(Factory.Grounded());
        //}

        if (Ctx.CharacterController.isGrounded && Ctx.ApliedMovementY <= 0.1f)
        {
            groundedConfirmTimer += Time.deltaTime;
            if (groundedConfirmTimer >= groundedConfirmThreshold)
            {
                SwitchState(Factory.Grounded());
                return;
            }
        }
        else
        {
            groundedConfirmTimer = 0f;
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

   public void HandleGravity()
    {

        float previousYVelocity = Ctx.CurrentMovementY;
        
            Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
            Ctx.ApliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, -20f);


    }

}
