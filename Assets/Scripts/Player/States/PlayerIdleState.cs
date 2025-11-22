using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Idle");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input) > 0.1f)
        {
            player.SwitchState(player.RunState);
            return;
        }

        if (Input.GetButtonDown("Jump") && player.motor.IsGrounded())
        {
            player.SwitchState(player.JumpState);
            return;
        }

        if (!player.motor.IsGrounded())
        {
            player.SwitchState(player.FallState);
            return;
        }

        // quedarse quieto
        player.motor.Move(0);
    }

    public override void ExitState(PlayerStateMachine player) { }
}
