using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Stats")]
    [SerializeField] private float updateRate;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject attackHitBox;

    private Transform player;
    private NavMeshAgent nav;
    private Coroutine chaseRoutine;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void ActivateEnemy()
    {
        
        if (chaseRoutine == null)
        {
            chaseRoutine = StartCoroutine(Chase());
            animator.SetBool("Activated", true);
        }
           
    }

    public void DeactivateEnemy()
    {
        if (chaseRoutine != null)
        {
            StopCoroutine(chaseRoutine);
            chaseRoutine = null;
            animator.SetBool("Activated", false);
        }
    }

    private IEnumerator Chase()
    {
        while (true)
        {
            nav.SetDestination(player.position);

            if (!nav.pathPending && nav.remainingDistance <= attackRange)
            {
                Attack();
            }

            yield return new WaitForSeconds(updateRate);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void EnableHitBox()
    {
        attackHitBox.SetActive(true);
    }

    void DisableHitBox()
    {
        attackHitBox.SetActive(false);
    }
}