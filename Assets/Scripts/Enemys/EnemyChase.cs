using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Stats")]
    [SerializeField] private float updateRate;
    [SerializeField] private float attackRange;

    private Transform player;
    private NavMeshAgent nav;
    private Coroutine chaseRoutine;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    public void ActivateEnemy()
    {
        if (chaseRoutine == null) 
            chaseRoutine = StartCoroutine(Chase());
    }

    public void DeactivateEnemy()
    {
        if (chaseRoutine != null)
        {
            StopCoroutine(chaseRoutine);
            chaseRoutine = null;
        }
    }

    private IEnumerator Chase()
    {
        while (true)
        {
            nav.SetDestination(player.position);

            if (!nav.pathPending && nav.remainingDistance <= attackRange)
            {
                //Faltaria la logica del ataque
            }

            yield return new WaitForSeconds(updateRate);
        }
    }
}