using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    public float bounceForce = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión es con la isla o con el agua
        if (collision.gameObject.CompareTag("Isla") || collision.gameObject.CompareTag("agua"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplica una fuerza de rebote hacia arriba
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }
}


