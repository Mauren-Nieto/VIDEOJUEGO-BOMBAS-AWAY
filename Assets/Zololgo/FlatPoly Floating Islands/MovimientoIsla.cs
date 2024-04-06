using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoIsla : MonoBehaviour
{
    public float velocidadMovimiento = 1f;
    public Rigidbody rb; // Componente Rigidbody del objeto isla

    // Método para recibir la entrada de los controles de Xbox One
    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal"); // Obtener entrada horizontal (stick izquierdo)
        float movimientoVertical = Input.GetAxis("Vertical"); // Obtener entrada vertical (stick izquierdo)

        // Calcular el vector de dirección del movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);

        // Aplicar el movimiento multiplicado por la velocidad
        rb.AddForce(movimiento * velocidadMovimiento);
    }
}
