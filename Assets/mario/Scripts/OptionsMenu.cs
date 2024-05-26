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

    // Variable para controlar si la música está silenciada
    private bool isMusicMuted = false;

    // Referencia al Coroutine para detenerlo si es necesario
    private Coroutine creditsCoroutine;

    private void Start()
    {
        // Configurar las acciones de los toggles
        musicOnToggle.onValueChanged.AddListener(delegate { ToggleMusic(true); });
        musicOffToggle.onValueChanged.AddListener(delegate { ToggleMusic(false); });

        // Configurar el estado inicial de los toggles según la música
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
            Debug.LogWarning("BackgroundMusicManager.Instance no está inicializado.");
        }
    }

    private void Update()
    {
        // Detectar la tecla Escape para volver al menú de opciones desde los créditos
        if (Input.GetKeyDown(KeyCode.Escape) && creditsPanel.activeSelf)
        {
            HideCredits();
        }
    }

    // Método para controlar el estado de la música según los toggles
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
            Debug.LogWarning("BackgroundMusicManager.Instance no está inicializado.");
        }
    }

    // Método para mostrar los créditos
    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            // Desactivar la música si está reproduciéndose
            if (!isMusicMuted && BackgroundMusicManager.Instance != null)
            {
                BackgroundMusicManager.Instance.StopMusic();
                isMusicMuted = true;
            }

            // Mostrar los créditos y comenzar la rutina para ocultarlos después de un tiempo
            creditsPanel.SetActive(true);
            creditsCoroutine = StartCoroutine(HideCreditsAfterDelay());
        }
        Debug.Log("Mostrar pantalla de créditos");
    }

    // Rutina para ocultar los créditos después de un tiempo
    private IEnumerator HideCreditsAfterDelay()
    {
        yield return new WaitForSeconds(creditsDuration);

        // Ocultar los créditos y reanudar la música si estaba reproduciéndose
        HideCredits();
        if (BackgroundMusicManager.Instance != null && isMusicMuted)
        {
            BackgroundMusicManager.Instance.PlayMusic();
            isMusicMuted = false;
        }
    }

    // Método para ocultar los créditos
    private void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
            // Detener la rutina si todavía está en curso
            if (creditsCoroutine != null)
            {
                StopCoroutine(creditsCoroutine);
            }
        }
        Debug.Log("Ocultar pantalla de créditos");
    }

    // Método para volver al menú principal
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}







