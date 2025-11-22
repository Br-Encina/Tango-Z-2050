using UnityEngine;

public class EnemyIdle : MonoBehaviour
{
    [Header("Detection Stats")]
    [SerializeField] private float detectionRange;

    private Transform player;
    private EnemyChase enemyChase;
    private bool activated = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChase = GetComponent<EnemyChase>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (!activated && distance <= detectionRange)
        {
            enemyChase.ActivateEnemy();
            activated = true; 
        }
    }
}
