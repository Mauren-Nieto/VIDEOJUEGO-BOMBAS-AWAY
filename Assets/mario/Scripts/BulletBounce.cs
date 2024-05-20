using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    public float bounceForce = 2f;
    public AudioClip bounceSound;

    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Isla") || collision.gameObject.CompareTag("Player"))
        {
            // Reproduce el sonido de rebote si hay
            if (bounceSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }

            // Aplica una fuerza de rebote
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = bounceDirection * bounceForce;

            // Hacer que la bala explote al rebotar
            BulletExplosion explosion = GetComponent<BulletExplosion>();
            if (explosion != null)
            {
                explosion.Explode();
            }
        }
    }
}






