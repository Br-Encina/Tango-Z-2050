using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerBaseState/*, IRootState*/
{
    public PlayerAimState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsAimingHash, true);

        InitializeSubState();
    }

    public override void UpdateState()
    {
        RotateArmsTowardMouse();
        CheckSwichStates();
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsAimingHash, false);
    }

    public override void CheckSwichStates()
    {
        if (!Ctx.IsAimPressed)
        {
            SwitchState(Factory.Grounded());
            return;
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsShootPressed)
            SetSubState(Factory.Shoot());
        else
            SetSubState(Factory.AimIdle());
    }

    void RotateArmsTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, Ctx.transform.position);

        if (plane.Raycast(ray, out float dist))
        {
            Vector3 hitPoint = ray.GetPoint(dist);
            Vector3 dir = hitPoint - Ctx.transform.position;
            dir.y = 0;

            Ctx.transform.rotation = Quaternion.Slerp(
                Ctx.transform.rotation,
                Quaternion.LookRotation(dir),
                10 * Time.deltaTime
            );
        }
    }
}
