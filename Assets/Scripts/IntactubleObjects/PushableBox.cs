using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableBox : MonoBehaviour
{
    public float pushForce = 5f;
    private bool isFalling;
    private PlayerStateMachine player;
    Collider Collider;

    Rigidbody rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;
        player = FindAnyObjectByType<PlayerStateMachine>();
    }


    public void Push(Vector3 direction)
    {
        
        //rb.AddForce(direction * pushForce, ForceMode.Force);
        rb.MovePosition(transform.position + direction * Time.deltaTime);
    }

    public IEnumerator Falling()
    {
       
            rb.isKinematic = false;
            isFalling = true;

            yield return new WaitForSeconds(2);

            rb.isKinematic = true;
            isFalling = false;
    }

    public void TriggerFall()
    {
        StartCoroutine(Falling());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFalling) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyChase enemy = collision.gameObject.GetComponent<EnemyChase>();

            enemy.DeactivateEnemy();
        }

        if (collision.gameObject.CompareTag("BoxPlayer"))
        {
            rb.isKinematic = true;
            if (player != null)
            {
                player.IsDead = true;
                
            }
        }
    }
}
