using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Text gameOverText; // Asignar en el Inspector
    public Text timerText; // Asignar en el Inspector
    public float gameDuration = 60f; // Duración del juego en segundos

    private float timeRemaining;
    private bool gameEnded = false;

    private Mario2 mario;
    private Mario2 luigi;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timeRemaining = gameDuration;

        // Encuentra a los personajes en la escena
        mario = GameObject.FindGameObjectWithTag("Mario").GetComponent<Mario2>();
        luigi = GameObject.FindGameObjectWithTag("Luigi").GetComponent<Mario2>();

        // Asegúrate de que los textos estén activos desde el inicio
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (!gameEnded)
        {
            timeRemaining -= Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
            }

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        gameEnded = true;

        if (mario.vidas <= 0 && luigi.vidas <= 0)
        {
            gameOverText.text = "Game Over! Both players lost!";
        }
        else if (mario.vidas > luigi.vidas)
        {
            gameOverText.text = "Player 1 (Mario) is the winner!";
        }
        else if (luigi.vidas > mario.vidas)
        {
            gameOverText.text = "Player 2 (Luigi) is the winner!";
        }
        else
        {
            gameOverText.text = "It's a tie!";
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        // Detener el lanzamiento de balas
        Bullet_Launcher1[] launchers = FindObjectsOfType<Bullet_Launcher1>();
        foreach (Bullet_Launcher1 launcher in launchers)
        {
            launcher.enabled = false;
        }
    }

    public void PlayerHit(Mario2 player)
    {
        player.RestarVida(1);
        if (player.vidas <= 0)
        {
            EndGame();
        }
    }

    public bool GameEnded
    {
        get { return gameEnded; }
    }
}










