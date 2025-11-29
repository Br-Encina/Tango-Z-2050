using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    PlayerStateMachine player;

    private void Awake()
    {
        player = GetComponent<PlayerStateMachine>();
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        Debug.Log($"Player received {amount} damage. Current Health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        player.IsDead = true;  // <- activa la transición en la state machine
    }
}
