using UnityEngine;
using UnityEngine.Events;

public class LeverInteractable : InteractBase
{
    [Header("Opcional: animación")]
    Animator leverAnimator;
    [SerializeField] GameObject door;




    bool isUsed = false;

    private void Start()
    {
        leverAnimator = GetComponent<Animator>();
    }

    public override void OnInteract(PlayerStateMachine player)
    {
        if (isUsed) return; 

        isUsed = true;

        // animación opcional
        if (leverAnimator != null)
            leverAnimator.SetTrigger("Activate");

        
       

        Debug.Log("Palanca activada!");

        base.OnInteract(player);
    }

   
}
