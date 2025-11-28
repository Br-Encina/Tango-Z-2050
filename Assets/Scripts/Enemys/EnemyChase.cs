using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour, IInteractuable
{
    [Header("Chase Stats")]
    [SerializeField] private float updateRate;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject attackHitBox;

    private bool attacking = false;

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
            float dist = Vector3.Distance(transform.position, player.position);
 
            if (dist < attackRange)
            {
                nav.isStopped = true;
                Attack();
            }
            else
            {
                nav.isStopped = false;
                nav.SetDestination(player.position);
            }

                yield return new WaitForSeconds(updateRate);
        }
    }

    void Attack()
    {
        if (attacking) return;

        attacking = true;
        animator.SetBool("Attack", attacking);
    }

    void EndAttack()
    {
        attacking = false;
        animator.SetBool("Attack", attacking);
    }

    void EnableHitBox()
    {
        attackHitBox.SetActive(true);
    }

    void DisableHitBox()
    {
        attackHitBox.SetActive(false);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void IInteractuable.OnShootHit()
    {
        Death();
    }
}