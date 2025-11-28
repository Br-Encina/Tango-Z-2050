using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IInteractuable
{
    public float explosionRange = 10f;

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRange);

        foreach (Collider hit in hits)
        {
            IInteractuable dmg = hit.GetComponent<IInteractuable>();

            if (dmg != null)
            {
                dmg.OnShootHit();
            }
        }
    }

    void IInteractuable.OnShootHit()
    {
        Explode();
    }
}
