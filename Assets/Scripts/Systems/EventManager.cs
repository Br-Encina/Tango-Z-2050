using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class Int2Event : UnityEvent<int, int>
{

}
public class EventManager : MonoBehaviour
{
    #region Singleton
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }

    }


    #endregion


    #region Shooting Events
    public UnityEvent OnShootEvent = new UnityEvent();
    #endregion

    public UnityEvent OnPushEvent = new UnityEvent();
}
