using System.Collections;
using UnityEngine;

public class Falling_Plataform : MonoBehaviour
{
    private bool falled = false;
    [SerializeField] private float timer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (falled) return;

        if (other.gameObject.CompareTag("BoxPlayer"))
        {
            //StartCoroutine(FallingPlataform());
            rb.isKinematic = false;
            falled = true;
        }
    }

    private IEnumerator FallingPlataform()
    {
        yield return new WaitForSeconds(timer);
        rb.isKinematic = false;
    }
}