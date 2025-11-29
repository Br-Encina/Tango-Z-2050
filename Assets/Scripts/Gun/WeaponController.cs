using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Transform nozzle;

    [SerializeField] GameObject flashEffect;
    [SerializeField] AudioClip[] audioClips  = new AudioClip[2];
    AudioSource audioShoot;

    private void Start()
    {
        nozzle = transform.Find("Nozzle");
        audioShoot = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnShootEvent.AddListener(Shoot);
        EventManager.Instance.BulletEmpty.AddListener(PlayNoAmmoSound);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnShootEvent.AddListener(Shoot);
        EventManager.Instance.BulletEmpty.RemoveListener(PlayNoAmmoSound);
    }
    public void Shoot()
    {
        GameObject flashClone = Instantiate(flashEffect, nozzle.position, nozzle.rotation);
        Destroy(flashClone, 0.1f);
        audioShoot.PlayOneShot(audioClips[0]);
    }

    public void PlayNoAmmoSound()
    {
        
        

            audioShoot.PlayOneShot(audioClips[1]);
        
        
    }
}
