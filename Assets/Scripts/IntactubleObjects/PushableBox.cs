using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableBox : MonoBehaviour
{
    public float pushForce = 5f;

    Rigidbody rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;
    }


    public void Push(Vector3 direction)
    {
        
        //rb.AddForce(direction * pushForce, ForceMode.Force);
        rb.MovePosition(transform.position + direction * Time.deltaTime);
    }

    public IEnumerator Falling()
    {
       
            rb.isKinematic = false;
            yield return new WaitForSeconds(2);
            rb.isKinematic = true;
        
    }

    public void TriggerFall()
    {
        StartCoroutine(Falling());
    }
}
