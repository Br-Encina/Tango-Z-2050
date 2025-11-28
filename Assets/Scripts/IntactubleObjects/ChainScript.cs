using UnityEngine;

public class ChainScript : MonoBehaviour, IInteractuable
{
    public float detectionRange = 5f;

    private void ChainBreak()
    {
        Collider[] boxesInRange = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider box in boxesInRange)
        {
            PushableBox dmg = box.GetComponent<PushableBox>();

            if (dmg != null)
            {
                dmg.TriggerFall();
            }
        }
    }

    void IInteractuable.OnShootHit()
    {
        ChainBreak();
    }
}
