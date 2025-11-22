using UnityEngine;
public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        player.motor.ApplyJump();
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (player.motor.VerticalVelocity() < 0)
        {
            player.SwitchState(player.FallState);
            return;
        }

        player.motor.Move(input);
    }

    public override void ExitState(PlayerStateMachine player) { }
}
