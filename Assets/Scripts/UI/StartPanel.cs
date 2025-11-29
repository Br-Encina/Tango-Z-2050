using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    void Start()
    {
        // Activar panel al inicio
        startPanel.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;
    }

    public void CloseStartPanel()
    {
        // Desactivar el panel
        startPanel.SetActive(false);

        // Reanudar el juego
        Time.timeScale = 1f;
    }
}
