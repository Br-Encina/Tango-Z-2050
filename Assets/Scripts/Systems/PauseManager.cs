using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject pausePanel;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);

        // Cargar volumen guardado
        float musicVal = PlayerPrefs.GetFloat("Music", 0);
        float sfxVal = PlayerPrefs.GetFloat("Sfx", 0);

        musicSlider.value = musicVal;
        sfxSlider.value = sfxVal;

        audioMixer.SetFloat("Music", musicVal);
        audioMixer.SetFloat("Sfx", sfxVal);

        // Eventos sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music", value);
        PlayerPrefs.SetFloat("Music", value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("Sfx", value);
        PlayerPrefs.SetFloat("Sfx", value);
    }
}
