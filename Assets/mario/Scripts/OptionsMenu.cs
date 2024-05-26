using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // Referencias a los elementos de la interfaz de usuario
    public Toggle musicOnToggle;
    public Toggle musicOffToggle;
    public GameObject creditsPanel;
    public float creditsDuration = 5f;

    // Variable para controlar si la m�sica est� silenciada
    private bool isMusicMuted = false;

    // Referencia al Coroutine para detenerlo si es necesario
    private Coroutine creditsCoroutine;

    private void Start()
    {
        // Configurar las acciones de los toggles
        musicOnToggle.onValueChanged.AddListener(delegate { ToggleMusic(true); });
        musicOffToggle.onValueChanged.AddListener(delegate { ToggleMusic(false); });

        // Configurar el estado inicial de los toggles seg�n la m�sica
        if (BackgroundMusicManager.Instance != null)
        {
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
        else
        {
            Debug.LogWarning("BackgroundMusicManager.Instance no est� inicializado.");
        }
    }

    private void Update()
    {
        // Detectar la tecla Escape para volver al men� de opciones desde los cr�ditos
        if (Input.GetKeyDown(KeyCode.Escape) && creditsPanel.activeSelf)
        {
            HideCredits();
        }
    }

    // M�todo para controlar el estado de la m�sica seg�n los toggles
    private void ToggleMusic(bool isOn)
    {
        if (BackgroundMusicManager.Instance != null)
        {
            if (isOn)
            {
                BackgroundMusicManager.Instance.PlayMusic();
                isMusicMuted = false;
            }
            else
            {
                BackgroundMusicManager.Instance.StopMusic();
                isMusicMuted = true;
            }
        }
        else
        {
            Debug.LogWarning("BackgroundMusicManager.Instance no est� inicializado.");
        }
    }

    // M�todo para mostrar los cr�ditos
    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            // Desactivar la m�sica si est� reproduci�ndose
            if (!isMusicMuted && BackgroundMusicManager.Instance != null)
            {
                BackgroundMusicManager.Instance.StopMusic();
                isMusicMuted = true;
            }

            // Mostrar los cr�ditos y comenzar la rutina para ocultarlos despu�s de un tiempo
            creditsPanel.SetActive(true);
            creditsCoroutine = StartCoroutine(HideCreditsAfterDelay());
        }
        Debug.Log("Mostrar pantalla de cr�ditos");
    }

    // Rutina para ocultar los cr�ditos despu�s de un tiempo
    private IEnumerator HideCreditsAfterDelay()
    {
        yield return new WaitForSeconds(creditsDuration);

        // Ocultar los cr�ditos y reanudar la m�sica si estaba reproduci�ndose
        HideCredits();
        if (BackgroundMusicManager.Instance != null && isMusicMuted)
        {
            BackgroundMusicManager.Instance.PlayMusic();
            isMusicMuted = false;
        }
    }

    // M�todo para ocultar los cr�ditos
    private void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
            // Detener la rutina si todav�a est� en curso
            if (creditsCoroutine != null)
            {
                StopCoroutine(creditsCoroutine);
            }
        }
        Debug.Log("Ocultar pantalla de cr�ditos");
    }

    // M�todo para volver al men� principal
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}







