using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    private int count = 0;
    private bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider entered pressure button");
        count++;
        if (!pressed)
        {
            pressed = true;
            OnPressed.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
       

        
        if (count <= 0)
        {
            pressed = false;
            OnReleased.Invoke();
        }
    }
}
