using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Detener la música de fondo y cargar la escena del juego
        BackgroundMusicManager.Instance.StopMusic();
        SceneManager.LoadScene("DemoScene");
    }

    public void OpenOptions()
    {
        // Cargar la escena del menú de opciones
        SceneManager.LoadScene("OptionsScene");
    }

    public void ExitGame()
    {
        // Detener la música de fondo y salir de la aplicación
        BackgroundMusicManager.Instance.StopMusic(); // Detener la música antes de salir
        Application.Quit();

        Debug.Log("El juego se ha cerrado"); // Mensaje de depuración para asegurarse de que el método se está llamando
    }
}





