using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int healt = 20;

    private EnemyChase chase;

    private void Awake()
    {
        chase = GetComponent<EnemyChase>();
    }

    public void ReceiveDamage(int damage)
    {
        healt -= damage;

        if (healt < 1)
        {
            chase.DeactivateEnemy();
        }
    }
}
