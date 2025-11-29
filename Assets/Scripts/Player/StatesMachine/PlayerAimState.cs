using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerBaseState/*, IRootState*/
{

    float armSpeed;
    float armAngle;
    public PlayerAimState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        if (!Ctx.IsMovementPressed) {
            Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);

            
        } 
        if (!Ctx.IsRunPressed && !Ctx.IsMovementPressed) {
            Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        }
        
            Ctx.Animator.SetBool(Ctx.IsAimingHash, true);

        InitializeSubState();
    }

    public override void UpdateState()
    {
        if (!Ctx.IsMovementPressed)
        {
            Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);


        }
        if (!Ctx.IsRunPressed && !Ctx.IsMovementPressed)
        {
            Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        }
        if (Ctx.IsMovementPressed && !Ctx.IsAimPressed)
        {
           
            if (Ctx.IsRunPressed)
            {
                Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
                Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
                
            } else
                Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        }

            Ctx.ApliedMovementZ = 0;
        
        RotateArmsTowardMouse();
        //RotateArms();
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
        if (Ctx.IsDead)
        {
            SwitchState(Factory.Death());
            return;
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsShootPressed)
        {
            SetSubState(Factory.Shoot());
        }
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

            //Ctx.transform.rotation = Quaternion.Slerp(
            //    Ctx.transform.rotation,
            //    Quaternion.LookRotation(dir),
            //    10 * Time.deltaTime
            //);
        }


    }

    //void RotateArms()
    //{
    //    armAngle += Input.GetAxis("Mouse Y") * armSpeed * Time.deltaTime;
    //    armAngle = Mathf.Clamp(armAngle, -10f, 10f);
    //    Ctx.Spine2.localRotation = Quaternion.Euler(-armAngle,0f,0f);

    //}
}
