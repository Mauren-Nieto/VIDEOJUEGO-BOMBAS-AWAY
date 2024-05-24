using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Text gameOverText;
    public Text timerText;
    public float gameDuration = 60f;

    private float timeRemaining;
    private bool gameEnded = false;

    private Mario2 mario;
    private Mario2 luigi;

    public AudioClip marioWinClip;
    public AudioClip luigiWinClip;
    public AudioClip gameOverClip;
    public AudioClip tieClip;
    private AudioSource audioSource;

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

        mario = GameObject.FindGameObjectWithTag("Mario").GetComponent<Mario2>();
        luigi = GameObject.FindGameObjectWithTag("Luigi").GetComponent<Mario2>();

        gameOverText.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!gameEnded && gameOverText != null)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
            }
            else
            {
                timeRemaining = 0;
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        gameEnded = true;

        // Detener la música de fondo
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        if (mario.vidas <= 0 && luigi.vidas <= 0)
        {
            gameOverText.text = "Game Over! Both players lost!";
            PlayGameOverAudio();
        }
        else if (mario.vidas > luigi.vidas)
        {
            gameOverText.text = "Mario is the winner!";
            PlayVictoryAudio(marioWinClip);
        }
        else if (luigi.vidas > mario.vidas)
        {
            gameOverText.text = "Luigi is the winner!";
            PlayVictoryAudio(luigiWinClip);
        }
        else
        {
            gameOverText.text = "It's a tie!";
            PlayTieAudio();
        }

        gameOverText.gameObject.SetActive(true);

        Bullet_Launcher1[] launchers = Object.FindObjectsOfType<Bullet_Launcher1>();
        foreach (var launcher in launchers)
        {
            launcher.enabled = false;
        }

        // Detener y destruir todas las balas y efectos de explosión
        BulletCollisionHandler[] bullets = FindObjectsOfType<BulletCollisionHandler>();
        foreach (var bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }

        GameObject[] explosions = GameObject.FindGameObjectsWithTag("ExplosionEffect");
        foreach (var explosion in explosions)
        {
            Destroy(explosion);
        }
    }

    private void PlayVictoryAudio(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void PlayGameOverAudio()
    {
        if (gameOverClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }
    }

    private void PlayTieAudio()
    {
        if (tieClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(tieClip);
        }
    }

    public bool GameEnded
    {
        get { return gameEnded; }
    }

    public void PlayerHit(Mario2 player)
    {
        player.RestarVida(1);
        if (player.vidas <= 0)
        {
            EndGame();
        }
    }

    public void RestartGame()
    {
        // Detener la música de fondo
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Reiniciar el tiempo del juego
        timeRemaining = gameDuration;

        // Reiniciar las posiciones y vidas de los personajes
        if (mario != null)
        {
            mario.RestartPosition();
        }

        if (luigi != null)
        {
            luigi.RestartPosition();
        }

        // Ocultar el texto de fin de juego
        gameOverText.gameObject.SetActive(false);

        // Reiniciar el estado del juego
        gameEnded = false;

        // Reiniciar el estado de los lanzadores de balas
        Bullet_Launcher1[] launchers = Object.FindObjectsOfType<Bullet_Launcher1>();
        foreach (var launcher in launchers)
        {
            launcher.enabled = true;
        }

        // Detener y destruir todas las balas y efectos de explosión
        BulletCollisionHandler[] activeBullets = FindObjectsOfType<BulletCollisionHandler>();
        foreach (var bullet in activeBullets)
        {
            Destroy(bullet.gameObject);
        }

        GameObject[] explosions = GameObject.FindGameObjectsWithTag("ExplosionEffect");
        foreach (var explosion in explosions)
        {
            Destroy(explosion);
        }

        // Reiniciar el texto del juego
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(gameDuration).ToString();
        }
    }
}










































































