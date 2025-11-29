using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class ShootButton : MonoBehaviour, IInteractuable
{
    [Header("Acciones al disparar")]
    public UnityEvent onButtonShot;
    Animator animator;
    bool isUsed = false;
    AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void OnShootHit()
    {
        if (isUsed) return;

        isUsed = true;

        audioS.Play();
        animator.SetBool("isUsed", true);
        onButtonShot?.Invoke();
    }
}
