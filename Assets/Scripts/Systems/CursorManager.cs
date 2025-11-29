using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor cuando apunta a un interactuable")]
    public Texture2D interactCursor;

    [Header("Hotspot (opcional)")]
    public Vector2 hotspot = Vector2.zero;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;

        // Asegurar que el cursor arranque con el original del sistema
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        HandleCursor();
    }

    void HandleCursor()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            var interact = hit.collider.GetComponent<IInteractuable>();

            if (interact != null)
            {
                // Cambia al cursor especial de interacción
                Cursor.SetCursor(interactCursor, hotspot, CursorMode.Auto);
                return;
            }
        }

        // Si no hay nada interactuable → vuelve al cursor normal del sistema
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
