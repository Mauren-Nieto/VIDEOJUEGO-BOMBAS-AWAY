using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    public float bounceForce = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Isla"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 bounceDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                rb.velocity = bounceDirection * bounceForce;
            }
        }
    }
}



