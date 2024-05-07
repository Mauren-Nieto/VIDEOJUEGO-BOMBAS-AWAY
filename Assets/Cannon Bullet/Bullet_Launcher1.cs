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
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBullet();
        }
    }

    void LaunchBullet()
    {
        // Calcula un ángulo aleatorio dentro del rango permitido
        float launchAngle = Random.Range(-maxLaunchAngle, maxLaunchAngle);
        Quaternion rotation = Quaternion.Euler(launchAngle, 0f, 0f);

        GameObject bullet = Instantiate(bulletPrefab, LaunchPoint.position, rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = LaunchPoint.forward * launchSpeed;

        // Agrega componente de explosión
        BulletExplosion explosion = bullet.AddComponent<BulletExplosion>();
        explosion.minTimeToExplode = minTimeToExplode;

        // Agrega componente de rebote
        BulletBounce bounce = bullet.AddComponent<BulletBounce>();
        bounce.bounceForce = bounceForce;
    }
}

