using UnityEngine;
using UnityEngine.Events;

public class LeverInteractable : InteractBase
{
    [Header("Opcional: animación")]
    public Animator leverAnimator;

   
   

    bool isUsed = false;

    public override void OnInteract(PlayerStateMachine player)
    {
        if (isUsed) return; 

        isUsed = true;

        // animación opcional
        if (leverAnimator != null)
            leverAnimator.SetTrigger("Activate");

        
        EventManager.Instance.OnLeverActivated.Invoke();

        Debug.Log("Palanca activada!");

        base.OnInteract(player);
    }

   
}
