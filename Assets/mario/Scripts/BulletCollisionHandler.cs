using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    public GameObject explosionEffect; // Efecto de explosi�n
    private AudioSource audioSource;
    public AudioClip explosionSound;
    public float audioVolume = 1f;

    private bool hasExploded = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && !GameManager.Instance.GameEnded)
        {
            // Verificar colisi�n con personajes o la isla
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
    }

    public void Explode()
    {
        if (GameManager.Instance.GameEnded) return;

        hasExploded = true;

        // Instanciar el efecto de explosi�n
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(explosion, 3f); // Asegurar que la explosi�n se destruya despu�s de 3 segundos
        }

        // Reproducir sonido de explosi�n
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound, audioVolume);
        }

        // Destruir la bala
        Destroy(gameObject, 0.5f); // Delay para que el sonido se reproduzca
    }
}







