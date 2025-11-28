using UnityEngine;

public class WeaponPickup : InteractBase
{
    public GameObject weaponModel;
  
    private void Start()
    {
        
        weaponModel.SetActive(false);
    }

    public override void OnInteract(PlayerStateMachine player)
    {
        Debug.Log("Arma agarrada!");

        // Aquí agregás el arma al inventario, o al hand socket
        weaponModel.SetActive(true);
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
