using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class LeverInteractable : InteractBase
{
   
    Animator leverAnimator;
    public UnityEvent onLever;
    AudioSource audioSource;




    bool isUsed = false;

    private void Start()
    {
        leverAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteract(PlayerStateMachine player)
    {
        if (isUsed) return; 

        isUsed = true;
        

        
        


        onLever?.Invoke();
        audioSource.Play();
        Debug.Log("Palanca activada!");

        base.OnInteract(player);
    }

   
}
