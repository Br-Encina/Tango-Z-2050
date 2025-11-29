using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    private bool alreadyWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyWon) return;

        if (other.CompareTag("BoxPlayer"))
        {
            alreadyWon = true;
            ShowVictoryPanel();
        }
    }

    void ShowVictoryPanel()
    {
        Time.timeScale = 0f; // Pausa total del juego
        victoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // liberar mouse
        Cursor.visible = true;
    }
}
