using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mario : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 100.0f;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    // Definimos las fronteras de la isla
    public float limiteIslaX = 10f;
    public float limiteIslaZ = 10f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, movimientoVertical) * velocidadMovimiento * Time.deltaTime;

        // Aplicamos movimiento relativo al mundo
        controller.Move(transform.TransformDirection(movimiento));

        float rotacionHorizontal = Input.GetAxis("Mouse X") * velocidadRotacion * Time.deltaTime;
        transform.Rotate(0, rotacionHorizontal, 0);

        // Limitamos al personaje a las fronteras de la isla
        LimitarPosicion();

        // Actualizamos la animación
        if (movimiento.magnitude > 0)
        {
            animator.SetBool("caminando", true);
        }
        else
        {
            animator.SetBool("caminando", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("moneda"))
        {
            Destroy(other.gameObject);
            audioSource.Play();
        }
    }

    // Método para limitar la posición del personaje dentro de las fronteras de la isla
    private void LimitarPosicion()
    {
        Vector3 posicionActual = transform.position;

        // Limitamos la posición en el eje X
        if (posicionActual.x < -limiteIslaX)
        {
            posicionActual.x = -limiteIslaX;
        }
        else if (posicionActual.x > limiteIslaX)
        {
            posicionActual.x = limiteIslaX;
        }

        // Limitamos la posición en el eje Z
        if (posicionActual.z < -limiteIslaZ)
        {
            posicionActual.z = -limiteIslaZ;
        }
        else if (posicionActual.z > limiteIslaZ)
        {
            posicionActual.z = limiteIslaZ;
        }

        // Aplicamos la posición limitada al personaje
        transform.position = posicionActual;
    }
}
