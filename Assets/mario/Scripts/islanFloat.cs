using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFloat : MonoBehaviour
{
    public LowPolyWater.LowPolyWater waterScript;  // Referencia al script del agua
    public float floatHeight = 1.0f;  // Altura base de la isla encima del agua

    void Update()
    {
        if (waterScript != null)
        {
            // Obtener la posici�n de la isla
            Vector3 islandPosition = transform.position;

            // Calcular la altura de la ola en la posici�n de la isla
            float waveHeightAtIslandPosition = GetWaveHeightAtPosition(islandPosition);

            // Ajustar la posici�n vertical de la isla
            islandPosition.y = waveHeightAtIslandPosition + floatHeight;

            // Aplicar la nueva posici�n a la isla
            transform.position = islandPosition;
        }
    }

    float GetWaveHeightAtPosition(Vector3 position)
    {
        // Posici�n de la ola seg�n el sistema de olas
        Vector3 waveOriginPosition = waterScript.waveOriginPosition;

        // Calcular la distancia desde el origen de la ola
        float distance = Vector3.Distance(new Vector3(position.x, 0, position.z), waveOriginPosition);
        distance = (distance % waterScript.waveLength) / waterScript.waveLength;

        // Calcular la altura de la ola en la posici�n dada
        float waveHeight = waterScript.waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * waterScript.waveFrequency
            + (Mathf.PI * 2.0f * distance));

        return waveHeight;
    }
}

