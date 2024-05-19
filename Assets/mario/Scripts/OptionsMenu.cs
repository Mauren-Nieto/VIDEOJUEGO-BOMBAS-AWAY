using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle musicOnToggle;
    public Toggle musicOffToggle;
    public GameObject creditsPanel;

    private void Start()
    {
        // Configurar las acciones de los toggles
        musicOnToggle.onValueChanged.AddListener(delegate { ToggleMusic(true); });
        musicOffToggle.onValueChanged.AddListener(delegate { ToggleMusic(false); });

        // Configurar el estado inicial de los toggles según la música
        if (BackgroundMusicManager.Instance.IsPlaying())
        {
            musicOnToggle.isOn = true;
            musicOffToggle.isOn = false;
        }
        else
        {
            musicOnToggle.isOn = false;
            musicOffToggle.isOn = true;
        }
    }

    private void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            BackgroundMusicManager.Instance.PlayMusic();
        }
        else
        {
            BackgroundMusicManager.Instance.StopMusic();
        }
    }

    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true);
        }
        Debug.Log("Show credits screen");
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

