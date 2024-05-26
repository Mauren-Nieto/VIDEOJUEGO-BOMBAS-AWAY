using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Detener la m�sica de fondo y cargar la escena del juego
        BackgroundMusicManager.Instance.StopMusic();
        SceneManager.LoadScene("DemoScene");
    }

    public void OpenOptions()
    {
        // Cargar la escena del men� de opciones
        SceneManager.LoadScene("OptionsScene");
    }

    public void ExitGame()
    {
        // Detener la m�sica de fondo y salir de la aplicaci�n
        BackgroundMusicManager.Instance.StopMusic(); // Detener la m�sica antes de salir
        Application.Quit();

        Debug.Log("El juego se ha cerrado"); // Mensaje de depuraci�n para asegurarse de que el m�todo se est� llamando
    }
}





