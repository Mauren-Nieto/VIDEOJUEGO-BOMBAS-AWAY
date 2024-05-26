using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    //Isntancia para la musica de fondo
    public static BackgroundMusicManager Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        //Sino hay una instancia en la musica de fondo, establecer esta como la instancia.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// Hacer que este objeto persista entre las escenas
            audioSource = GetComponent<AudioSource>();// Obtener el componente AudioSource adjunto a este GameObject
        }
        else
        {
            Destroy(gameObject);// Destruir cualquier instancia duplicada
        }
    }

    //Metodo para reproducir la musica
    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    //Metodo para detener la musica
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Método para verificar si la musica esta reproduciendose
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}






