using UnityEngine;

public class Ladder : MonoBehaviour
{
    Transform alignPoint;

    private void Start()
    {
        alignPoint = transform.GetChild(0);
    }
}