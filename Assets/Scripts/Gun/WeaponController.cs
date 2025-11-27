using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Transform nozzle;

    [SerializeField] GameObject flashEffect;
    AudioSource audioShoot;

    private void Start()
    {
        nozzle = transform.Find("Nozzle");
        audioShoot = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnShootEvent.AddListener(Shoot);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnShootEvent.AddListener(Shoot);
    }
    public void Shoot()
    {
        GameObject flashClone = Instantiate(flashEffect, nozzle.position, nozzle.rotation);
        Destroy(flashClone, 0.1f);
        audioShoot.Play();
    }
}
