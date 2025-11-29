using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Textures")]
    public Texture2D defaultCursor;
    public Texture2D interactCursor;

    [Header("Cursor Hotspot")]
    public Vector2 hotspot = Vector2.zero;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;

        // Cursor predeterminado
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
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
                // Cambia al cursor de interacción
                Cursor.SetCursor(interactCursor, hotspot, CursorMode.Auto);
                return;
            }
        }

        // Si no hay interactuable bajo el mouse → cursor normal
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}
