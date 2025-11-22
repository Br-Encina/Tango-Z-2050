using UnityEngine;
public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Run");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(input) < 0.1f)
        {
            player.SwitchState(player.IdleState);
            return;
        }

        if (!player.motor.IsGrounded())
        {
            player.SwitchState(player.FallState);
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.SwitchState(player.JumpState);
            return;
        }

        player.motor.Move(input);
    }

    public override void ExitState(PlayerStateMachine player) { }
}
