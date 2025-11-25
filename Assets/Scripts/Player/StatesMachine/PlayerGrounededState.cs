using UnityEngine;

public class PlayerGrounededState : PlayerBaseState, IRootState
{

    float notGroundedTimer = 0f;
    const float notGroundedThreshold = 0.06f;

    public PlayerGrounededState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
       IsRootState = true;
       
    }
    public void HandleGravity()
    {
       
        
            Ctx.CurrentMovementY = Ctx.Gravity;
            Ctx.ApliedMovementY = Ctx.Gravity;
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        //HandleGravity();
        Ctx.CurrentMovementY = Mathf.Min(Ctx.CurrentMovementY, Ctx.Gravity);
        notGroundedTimer = 0f;
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
        //if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        //{
        //    SwitchState(Factory.Jump());
        //}
        //else if (!Ctx.CharacterController.isGrounded)
        //{
        //    SwitchState(Factory.Fall());
        //}
        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
            return;
        }
        
       
        
        if (!Ctx.CharacterController.isGrounded)
        {
            notGroundedTimer += Time.deltaTime;
            if (notGroundedTimer >= notGroundedThreshold)
            {
                SwitchState(Factory.Fall());
                return;
            }
        }
        else
        {
            notGroundedTimer = 0f;
        }
        if (Ctx.IsTouchingPushBox && Ctx.IsPushPressed && Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Push());
            return;
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
}
