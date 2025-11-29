using UnityEngine;

public class WeaponPickup : InteractBase
{
    public GameObject weaponModel;
    public GameObject weaponInfo;
    AudioSource audioSource;
  
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weaponInfo.SetActive( false);
        weaponModel.SetActive(false);
    }

    public override void OnInteract(PlayerStateMachine player)
    {
        Debug.Log("Arma agarrada!");

        // Aquí agregás el arma al inventario, o al hand socket
        weaponModel.SetActive(true);
        weaponInfo.SetActive( true);
        audioSource.Play();
        //EventManager.Instance.OnPickGunEvent.Invoke();
        player.GunIsPickeded = true;

        base.OnInteract(player);
        Destroy(this);
    }

    //public override string GetInteractText()
    //{
    //    return "Press E to Pick Up Weapon";
    //}
}
