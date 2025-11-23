using UnityEngine;

public class PlayerGrounededState : PlayerBaseState
{

    public PlayerGrounededState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
       
    }
    public override void EnterState()
    {
        Debug.Log("Entered Grounded State");
    }
    public override void UpdateState()
    {

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
