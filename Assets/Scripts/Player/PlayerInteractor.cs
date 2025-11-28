using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInteractor : MonoBehaviour
{
    [Header("Detection")]
    public float interactRange = 1.5f;
    public LayerMask interactLayer;

    [Header("References")]
    public InteractionUI ui;

    IInteract currentInteractable;
    Collider[] buffer = new Collider[8];

    private void Update()
    {
        DetectInteractable();
        HandleInput();
    }

    void HandleInput()
    {
        if (currentInteractable == null) return;

        // Soporte Input System si está activo, si no usa la vieja api
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null ? Keyboard.current.eKey.wasPressedThisFrame : Input.GetKeyDown(KeyCode.E))
#else
        if (Input.GetKeyDown(KeyCode.E))
#endif
        {
            Debug.Log("[Interactor] Interact pressed with " + ((MonoBehaviour)currentInteractable).gameObject.name);
            currentInteractable.OnInteract(GetComponent<PlayerStateMachine>());
        }
    }

    void DetectInteractable()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, interactRange, buffer, interactLayer);
        if (hits <= 0)
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
                if (ui != null) ui.Hide();
                Debug.Log("[Interactor] No interactable in range.");
            }
            return;
        }

        // Priorizar el hit más cercano
        float minDist = float.MaxValue;
        IInteract found = null;
        for (int i = 0; i < hits; i++)
        {
            var col = buffer[i];
            var interact = col.GetComponent<IInteract>();
            if (interact == null) continue;

            float d = Vector3.Distance(transform.position, col.transform.position);
            if (d < minDist)
            {
                minDist = d;
                found = interact;
            }
        }

        if (found != null)
        {
            if (currentInteractable == null || ((MonoBehaviour)found).gameObject != ((MonoBehaviour)currentInteractable).gameObject)
            {
                currentInteractable = found;
                if (ui != null) ui.Show(found.GetInteractText());
                Debug.Log("[Interactor] Found interactable: " + ((MonoBehaviour)found).gameObject.name);
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
                if (ui != null) ui.Hide();
                Debug.Log("[Interactor] Found nothing interactable in hits.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
