using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(25);

        }
    }
}
