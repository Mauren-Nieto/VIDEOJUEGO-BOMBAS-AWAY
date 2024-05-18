using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Launcher1 : MonoBehaviour
{
    public Transform LaunchPoint;
    public GameObject bulletPrefab;
    public float launchSpeed = 10f;
    public float maxLaunchAngle = 20f;
    public float minTimeToExplode = 0.5f;
    public float bounceForce = 2f;

    public AudioClip launchSound; // Sonido del lanzamiento de la bomba
    public AudioClip explosionSound; // Sonido de la explosión de la bomba

    private AudioSource audioSource;
    public float launchInterval = 2f; // Intervalo entre lanzamientos
    private float launchTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        launchTimer = launchInterval;
    }

    void Update()
    {
        if (!GameManager.Instance.GameEnded)
        {
            launchTimer -= Time.deltaTime;
            if (launchTimer <= 0f)
            {
                LaunchBullet();
                launchTimer = launchInterval;
            }
        }
    }

    void LaunchBullet()
    {
        if (launchSound != null)
        {
            audioSource.PlayOneShot(launchSound);
        }

        float launchAngle = Random.Range(-maxLaunchAngle, maxLaunchAngle);
        Quaternion rotation = Quaternion.Euler(launchAngle, 0f, 0f);

        GameObject bullet = Instantiate(bulletPrefab, LaunchPoint.position, rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = LaunchPoint.forward * launchSpeed;

        BulletExplosion explosion = bullet.AddComponent<BulletExplosion>();
        explosion.minTimeToExplode = minTimeToExplode;
        explosion.explosionSound = explosionSound;

        BulletBounce bounce = bullet.AddComponent<BulletBounce>();
        bounce.bounceForce = bounceForce;
    }
}



























