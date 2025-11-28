using UnityEngine;

public class PlayerRailConstraint : MonoBehaviour
{
    [Header("Rail Settings")]
    public bool railEnabled = true;          
    public float targetX = 0f;               
    public float smooth = 15f;               

    Transform player;

    private void Start()
    {
        player = transform;
        targetX = player.position.x; 
    }

    private void LateUpdate()
    {
        if (!railEnabled) return;

        Vector3 pos = player.position;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * smooth);
        player.position = pos;
    }

    // -------- API Pública (lo llamás desde tu PlayerStateMachine o donde quieras) --------

    /// <summary>
    /// Activa el rail, el personaje vuelve a targetX
    /// </summary>
    public void EnableRail()
    {
        railEnabled = true;
    }

    /// <summary>
    /// Desactiva el rail, permite movimiento libre en X
    /// </summary>
    public void DisableRail()
    {
        railEnabled = false;
    }

    /// <summary>
    /// Cambia la línea de movimiento a otra X
    /// (útil si el juego tiene varios "carriles")
    /// </summary>
    public void SetRailX(float newX)
    {
        targetX = newX;
    }

    /// <summary>
    /// Hacer snap instantáneo al rail sin suavizado
    /// </summary>
    public void SnapToRail()
    {
        Vector3 pos = player.position;
        pos.x = targetX;
        player.position = pos;
    }
}

