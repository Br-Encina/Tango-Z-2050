using UnityEngine;
public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Fall");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (player.motor.IsGrounded())
        {
            if (Mathf.Abs(input) > 0.1f)
                player.SwitchState(player.RunState);
            else
                player.SwitchState(player.IdleState);

            return;
        }

        player.motor.Move(input);
    }

    public override void ExitState(PlayerStateMachine player) { }
}
