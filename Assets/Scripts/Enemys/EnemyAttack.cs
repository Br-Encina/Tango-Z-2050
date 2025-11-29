using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoxPlayer"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null )
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.Log("No encontrado");
            }

        }
    }
}
