using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootState : PlayerBaseState
{
    float shootTimer;
    const float shootDuration = 0.15f;

    public PlayerShootState(PlayerStateMachine ctx, PlayerStateFactory factory)
        : base(ctx, factory) { }

    public override void EnterState()
    {
        Ctx.IsShooting = true;
        
        if (!Ctx.HasAmmo)
        {
            Debug.Log("Sin balas!");
            EventManager.Instance.BulletEmpty.Invoke();
            SwitchState(Factory.AimIdle());
            return;
        }
        Ctx.CurrentAmmo = Ctx.CurrentAmmo - 1;
        EventManager.Instance.OnShootEvent.Invoke();
        EventManager.Instance.UpdateBulletsEvent.Invoke(Ctx.CurrentAmmo, Ctx.MaxAmmo);
        ShootRay();

        shootTimer = shootDuration;
    }

    public override void UpdateState()
    {
        shootTimer -= Time.deltaTime;
        CheckSwichStates();
    }

    public override void ExitState() {
        Ctx.IsShooting = false;
        Ctx.IsShootPressed = false;

    }

    public override void CheckSwichStates()
    {
        if (shootTimer <= 0)
        {
            SwitchState(Factory.AimIdle());
        }
    }

    public override void InitializeSubState() { }

    void ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            var interact = hit.collider.GetComponent<IInteractuable>();
            if (interact != null)
            {
                interact.OnShootHit();
            }
        }
    }
}
