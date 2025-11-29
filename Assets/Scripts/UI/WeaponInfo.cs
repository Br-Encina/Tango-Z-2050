using TMPro;
using UnityEngine;
public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text currentBullets;
    [SerializeField] private TMP_Text totalBullets;


    private void OnEnable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.UpdateBulletsEvent.AddListener(UpdateBullet);
    }

    private void OnDisable()
    {

    }
    public void UpdateBullet(int newCurrentBullet, int newTotalBullet)
    {
        //newCurrentBullet = newCurrentBullet / 2;
        //newTotalBullet = newTotalBullet / 2;

        if (newCurrentBullet <= 0)
        {
            currentBullets.color = new Color(1, 0, 0);
        }
        else if (newCurrentBullet > 0)
        {
            currentBullets.color = Color.white;
        }
        currentBullets.text = newCurrentBullet.ToString();
        totalBullets.text = newTotalBullet.ToString();
    }


}