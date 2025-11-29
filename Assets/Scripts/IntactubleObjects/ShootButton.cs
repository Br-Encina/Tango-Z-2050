using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class ShootButton : MonoBehaviour, IInteractuable
{
    [Header("Acciones al disparar")]
    public UnityEvent onButtonShot;
    Animator animator;
    bool isUsed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnShootHit()
    {
        if (isUsed) return;

        isUsed = true;

        animator.SetBool("isUsed", true);
        onButtonShot?.Invoke();
    }
}
