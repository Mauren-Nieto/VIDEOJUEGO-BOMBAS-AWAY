using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float minTimeToExplode = 0.5f;
    public AudioClip explosionSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Agrega un AudioSource si no existe y config�ralo
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Asigna el clip de sonido de explosi�n
        audioSource.clip = explosionSound;

        // Invoca el m�todo Explode despu�s del tiempo especificado
        Invoke("Explode", minTimeToExplode);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mario") || collision.gameObject.CompareTag("Luigi"))
        {
            PlayExplosionSound();
            Mario2 player = collision.gameObject.GetComponent<Mario2>();
            if (player != null)
            {
                GameManager.Instance.PlayerHit(player);
            }
            Explode();
        }
        else if (collision.gameObject.CompareTag("Isla"))
        {
            PlayExplosionSound();
            Explode();
        }
    }

    private void PlayExplosionSound()
    {
        // Reproduce el sonido de explosi�n si existe un AudioSource y un clip de sonido
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }

    private void Explode()
    {
        // Destruye el objeto
        Destroy(gameObject);
    }
}




















