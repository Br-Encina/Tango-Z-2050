using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerBaseState currentState;

    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerFallState FallState = new PlayerFallState();

    [HideInInspector] public PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        SwitchState(IdleState);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        if (currentState != null)
            currentState.ExitState(this);

        currentState = newState;
        newState.EnterState(this);
    }
}
