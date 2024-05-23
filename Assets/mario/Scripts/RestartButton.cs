using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameManager gameManager; // Campo para almacenar la referencia al GameManager

    // M�todo que se llamar� cuando se haga clic en el bot�n de reinicio
    public void OnRestartButtonClicked()
    {
        // Verificar si se ha establecido una referencia al GameManager
        if (gameManager != null)
        {
            // Llamar al m�todo RestartGame() del GameManager para reiniciar el juego
            gameManager.RestartGame();
        }
        else
        {
            Debug.LogWarning("Game Manager reference is not set in the RestartButton script!");
        }
    }
}









