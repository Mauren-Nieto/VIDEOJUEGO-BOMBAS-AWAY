using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float minTimeToExplode = 0.5f;
    public AudioClip explosionSound;
    public float audioVolume = 1f; // Volumen de sonido

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(minTimeToExplode);
        Explode();
    }

    private void Explode()
    {
        // Reproducir sonido de explosión con el volumen ajustado
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound, audioVolume);
        }
        // Lógica de explosión...
        Destroy(gameObject);
    }
}


























