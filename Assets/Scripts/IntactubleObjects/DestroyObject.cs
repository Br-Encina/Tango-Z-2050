using UnityEngine;

public class DestroyObject : MonoBehaviour, IInteractuable
{
    public void OnShootHit()
    {
        Destroy(gameObject);
    }
}
