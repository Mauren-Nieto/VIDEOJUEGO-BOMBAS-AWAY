using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float minTimeToExplode = 0.5f;

    private void Start()
    {
        StartCoroutine(ExplodeAfterTime());
    }

    IEnumerator ExplodeAfterTime()
    {
        yield return new WaitForSeconds(minTimeToExplode);
        Explode();
    }

    void Explode()
    {
        // Lógica de explosión
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si la bala golpea un personaje, explota de inmediato
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
            // Aquí puedes implementar la lógica para que el personaje reviva en su posición inicial
        }
    }
}
