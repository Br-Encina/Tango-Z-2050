using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class LeverInteractable : InteractBase
{
   
    Animator leverAnimator;
    public UnityEvent onLever;




    bool isUsed = false;

    private void Start()
    {
        leverAnimator = GetComponent<Animator>();
    }

    public override void OnInteract(PlayerStateMachine player)
    {
        if (isUsed) return; 

        isUsed = true;
        

        
        


        onLever?.Invoke();

        Debug.Log("Palanca activada!");

        base.OnInteract(player);
    }

   
}
