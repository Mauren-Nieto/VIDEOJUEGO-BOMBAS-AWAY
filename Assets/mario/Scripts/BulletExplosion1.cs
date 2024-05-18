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
        Invoke("Explode", minTimeToExplode);
    }

    public void Explode()
    {
        PlayExplosionSound();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHit(collision.gameObject.GetComponent<Mario2>());
            Explode();
        }
        else if (collision.gameObject.CompareTag("Isla"))
        {
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
}

























