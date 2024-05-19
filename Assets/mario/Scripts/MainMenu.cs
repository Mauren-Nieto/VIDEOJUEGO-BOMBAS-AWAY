using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        BackgroundMusicManager.Instance.StopMusic();
        SceneManager.LoadScene("DemoScene");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void ExitGame()
    {
        BackgroundMusicManager.Instance.StopMusic(); // Detener la música antes de salir
        Application.Quit();

        Debug.Log("El juego se ha cerrado"); // Mensaje de depuración para asegurarse de que el método se está llamando.
    }
}



