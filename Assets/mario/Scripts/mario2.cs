using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 100.0f;
    public float fuerzaSalto = 8.0f;
    public float gravedad = 20.0f;
    public float limiteIslaX = 10f;
    public float limiteIslaZ = 10f;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;
    Vector3 movimiento = Vector3.zero;
    bool enElAire = false;

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

        // Movimiento horizontal y vertical
        movimiento = new Vector3(movimientoHorizontal, 0f, movimientoVertical);
        movimiento = transform.TransformDirection(movimiento);
        movimiento *= velocidadMovimiento;

        // Rotación con el mouse
        float rotacionHorizontal = Input.GetAxis("Mouse X") * velocidadRotacion * Time.deltaTime;
        transform.Rotate(0, rotacionHorizontal, 0);

        // Limitamos la posición dentro de los límites de la isla
        LimitarPosicion();

        // Saltar si presiona la tecla "Espacio" y está en el suelo
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            movimiento.y = fuerzaSalto;
            enElAire = true;
        }

        // Aplicar la gravedad
        if (!controller.isGrounded)
        {
            enElAire = true;
            movimiento.y -= gravedad * Time.deltaTime;
        }
        else if (enElAire)
        {
            enElAire = false;
            movimiento.y = 0;
        }

        // Mover al personaje
        controller.Move(movimiento * Time.deltaTime);

        // Actualizar la animación
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
        posicionActual.x = Mathf.Clamp(posicionActual.x, -limiteIslaX, limiteIslaX);

        // Limitamos la posición en el eje Z
        posicionActual.z = Mathf.Clamp(posicionActual.z, -limiteIslaZ, limiteIslaZ);

        // Aplicamos la posición limitada al personaje
        transform.position = posicionActual;
    }
}
