using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    public float bounceForce = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        // Simular el rebote cuando la bala golpea la superficie
        if (collision.gameObject.CompareTag("Ground"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 newVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = newVelocity.normalized * bounceForce;
        }
    }
}
