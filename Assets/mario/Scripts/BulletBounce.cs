using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    public float bounceForce = 2f;
    public AudioClip bounceSound;
    public float audioVolume = 1f; // Volumen de sonido

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Reproducir sonido de rebote con el volumen ajustado
        if (bounceSound != null)
        {
            audioSource.PlayOneShot(bounceSound, audioVolume);
        }
        // Lógica de rebote...
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }
}







