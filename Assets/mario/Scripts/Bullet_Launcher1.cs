using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet_Launcher1 : MonoBehaviour
{
    public Transform LaunchPoint;
    public GameObject bulletPrefab;
    public float launchSpeed = 10f;
    public float maxLaunchAngle = 45f; // Ángulo máximo de lanzamiento
    public float minTimeToExplode = 0.5f;
    public float bounceForce = 2f;

    public AudioClip launchSound; // Sonido del lanzamiento de la bomba
    public AudioClip explosionSound; // Sonido de la explosión de la bomba
    public AudioClip bounceSound; // Sonido del rebote de la bomba

    public float fireRate = 2f; // Tiempo en segundos entre disparos
    private float nextFireTime;

    public MeshCollider islandCollider;

    private AudioSource audioSource;

    // Variable de volumen para los sonidos
    [Range(0f, 1f)]
    public float audioVolume = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextFireTime = Time.time;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            LaunchBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void LaunchBullet()
    {
        // Reproducir sonido de lanzamiento con el volumen ajustado
        if (launchSound != null)
        {
            audioSource.PlayOneShot(launchSound, audioVolume);
        }

        // Calcula una posición aleatoria dentro del colisionador de la isla
        Vector3 randomPoint = GetRandomPointInCollider(islandCollider);

        // Calcula la dirección de lanzamiento con un ángulo aleatorio
        Vector3 direction = (randomPoint - LaunchPoint.position).normalized;
        float angle = Random.Range(-maxLaunchAngle, maxLaunchAngle);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        Vector3 launchDirection = rotation * direction;

        // Crea y lanza la bala
        GameObject bullet = Instantiate(bulletPrefab, LaunchPoint.position, Quaternion.LookRotation(launchDirection));
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = launchDirection * launchSpeed;

        // Agrega componente de explosión
        BulletExplosion explosion = bullet.AddComponent<BulletExplosion>();
        explosion.minTimeToExplode = minTimeToExplode;
        explosion.explosionSound = explosionSound;
        explosion.audioVolume = audioVolume; // Pasar el volumen al componente de explosión

        // Agrega componente de rebote
        BulletBounce bounce = bullet.AddComponent<BulletBounce>();
        bounce.bounceForce = bounceForce;
        bounce.bounceSound = bounceSound;
        bounce.audioVolume = audioVolume; // Pasar el volumen al componente de rebote
    }

    Vector3 GetRandomPointInCollider(MeshCollider collider)
    {
        Bounds bounds = collider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.max.y;

        return new Vector3(x, y, z);
    }
}
































