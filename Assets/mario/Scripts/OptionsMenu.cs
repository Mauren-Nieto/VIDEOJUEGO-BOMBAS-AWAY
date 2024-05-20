using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle musicOnToggle;
    public Toggle musicOffToggle;
    public GameObject creditsPanel;
    public float creditsDuration = 5f; // Duración de la animación de créditos

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

    private void Update()
    {
        // Detectar la tecla Escape para volver al menú de opciones
        if (Input.GetKeyDown(KeyCode.Escape) && creditsPanel.activeSelf)
        {
            HideCredits();
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
            StartCoroutine(HideCreditsAfterDelay());
        }
        Debug.Log("Show credits screen");
    }

    private IEnumerator HideCreditsAfterDelay()
    {
        yield return new WaitForSeconds(creditsDuration);
        HideCredits();
    }

    private void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }
        Debug.Log("Hide credits screen");
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

