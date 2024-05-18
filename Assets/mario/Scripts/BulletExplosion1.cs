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
        // Agrega un AudioSource si no existe y configúralo
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Asigna el clip de sonido de explosión
        audioSource.clip = explosionSound;

        // Invoca el método Explode después del tiempo especificado
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
        // Reproduce el sonido de explosión si existe un AudioSource y un clip de sonido
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




















