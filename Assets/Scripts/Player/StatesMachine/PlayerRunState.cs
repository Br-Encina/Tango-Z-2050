using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        
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
    }
    public override void InitializeSubState()
    {
    }

}
