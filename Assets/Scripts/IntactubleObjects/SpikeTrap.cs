using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpikeTrap : MonoBehaviour
{
    int damage = 9999; // daño letal

    Collider spikeCollider;
    private void Start()
    {
        spikeCollider = GetComponent<Collider>();
        spikeCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoxPlayer"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        if (other.CompareTag("Enemy"))
        {
            EnemyChase enemyChase = other.GetComponent<EnemyChase>();
            if (enemyChase != null)
            {
                enemyChase.DeactivateEnemy();
            }
        }
    }
}
