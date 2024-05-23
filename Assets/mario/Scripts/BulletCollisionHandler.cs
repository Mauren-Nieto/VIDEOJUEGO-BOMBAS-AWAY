using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    public GameObject explosionEffect; // Efecto de explosión
    private AudioSource audioSource;
    public AudioClip explosionSound;
    public float audioVolume = 1f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar colisión con personajes o la isla
        if (collision.gameObject.CompareTag("Mario") || collision.gameObject.CompareTag("Luigi") || collision.gameObject.CompareTag("Island"))
        {
            Explode();

            // Restar vida al personaje si es Mario o Luigi
            if (collision.gameObject.CompareTag("Mario") || collision.gameObject.CompareTag("Luigi"))
            {
                Mario2 character = collision.gameObject.GetComponent<Mario2>();
                character.RestarVida(1);

                if (character.vidas <= 0)
                {
                    GameManager.Instance.EndGame();
                }
            }
        }
    }

    private void Explode()
    {
        // Instanciar el efecto de explosión
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Reproducir sonido de explosión
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound, audioVolume);
        }

        // Destruir la bala
        Destroy(gameObject);
    }
}


