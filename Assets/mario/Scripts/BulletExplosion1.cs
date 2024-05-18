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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = explosionSound;
        Invoke("Explode", minTimeToExplode);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mario") || collision.gameObject.CompareTag("Luigi"))
        {
            PlayExplosionSound();
            GameManager.Instance.PlayerHit(collision.gameObject.GetComponent<Mario2>());
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
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}























