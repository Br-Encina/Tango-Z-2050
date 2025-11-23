using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        HandleJump();

    }
    public override void UpdateState()
    {
        CheckSwichStates();
        HandleGravity();
    }
    public override void ExitState()
    {
        _ctx.Animator.SetBool(_ctx.IsJumpingHash, false);
        //_ctx.IsJumpAnim = false;
        if (_ctx.IsJumpPressed)
        {
            _ctx.RequireNewJumpPress = true;
        }
    }
    public override void CheckSwichStates()
    {
        if (_ctx.CharacterController.isGrounded)
        {
            SwitchState(_factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {
    }

    void HandleJump()
    {
            _ctx.IsJumpPressed = false; 
            _ctx.IsJumping = true;
            _ctx.RequireNewJumpPress = true;

            _ctx.Animator.SetBool(_ctx.IsJumpingHash, true);

            _ctx.CurrentMovementY = _ctx.InitialJumpVelocity;
            _ctx.ApliedMovementY = _ctx.InitialJumpVelocity;
        
       

    }

    void HandleGravity()
    {
        bool isFalling = _ctx.CurrentMovementY > 0f || !_ctx.IsJumpPressed;
        float fallMultiplier = 2.0f;
        float previousY = _ctx.CurrentMovementY;

        if (isFalling)
        {
            
            _ctx.CurrentMovementY = _ctx.CurrentMovementY + (_ctx.Gravity * fallMultiplier * Time.deltaTime);
            _ctx.ApliedMovementY = Mathf.Max((previousY + _ctx.CurrentMovementY) * 0.5f, -20f);
        }
        else if(_ctx.CurrentMovementY > _ctx.Gravity)
        {
            _ctx.CurrentMovementY += _ctx.Gravity * Time.deltaTime;
            _ctx.ApliedMovementY = (previousY + _ctx.CurrentMovementY) * 0.5f;
        }

    }
}
