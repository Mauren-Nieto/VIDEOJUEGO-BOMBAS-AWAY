using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario2 : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float rotacion = 20.0f;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    public enum Personaje
    {
        Mario,
        Luigi
    }

    public Personaje personaje;

    // Referencia al MeshCollider de la isla
    public MeshCollider islaCollider;

    // Referencia al GameObject de la isla
    public Transform islaTransform;

    // Altura del agua
    public float alturaAgua = -1.0f;

    private Vector3 ultimaPosicionIsla;
    private Vector3 posicionInicial;

    // Vidas del personaje
    public int vidas = 3;

    private GameManager gameManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (islaTransform != null)
        {
            ultimaPosicionIsla = islaTransform.position;
        }

        // Guardar la posición inicial del personaje
        posicionInicial = transform.position;

        // Encontrar el GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager != null && !gameManager.GameEnded)
        {
            animator.SetBool("caminando", false);

            // Mover el personaje hacia adelante
            if (personaje == Personaje.Mario && Input.GetKey(KeyCode.W))
            {
                controller.Move(transform.forward * Time.deltaTime * velocidad);
                animator.SetBool("caminando", true);
            }
            else if (personaje == Personaje.Luigi && Input.GetKey(KeyCode.UpArrow))
            {
                controller.Move(transform.forward * Time.deltaTime * velocidad);
                animator.SetBool("caminando", true);
            }

            // Mover el personaje hacia atrás
            if (personaje == Personaje.Mario && Input.GetKey(KeyCode.S))
            {
                controller.Move(-transform.forward * Time.deltaTime * velocidad);
                animator.SetBool("caminando", true);
            }
            else if (personaje == Personaje.Luigi && Input.GetKey(KeyCode.DownArrow))
            {
                controller.Move(-transform.forward * Time.deltaTime * velocidad);
                animator.SetBool("caminando", true);
            }

            // Rotar el personaje a la derecha
            if (personaje == Personaje.Mario && Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up, rotacion * Time.deltaTime);
            }
            else if (personaje == Personaje.Luigi && Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(transform.up, rotacion * Time.deltaTime);
            }

            // Rotar el personaje a la izquierda
            if (personaje == Personaje.Mario && Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up, -rotacion * Time.deltaTime);
            }
            else if (personaje == Personaje.Luigi && Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(transform.up, -rotacion * Time.deltaTime);
            }

            // Sincronizar movimiento con la isla
            if (islaTransform != null)
            {
                Vector3 movimientoIsla = islaTransform.position - ultimaPosicionIsla;
                transform.position += movimientoIsla;
                ultimaPosicionIsla = islaTransform.position;
            }

            ChequearLimiteIsla();
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

    private void ChequearLimiteIsla()
    {
        Vector3 posicionActual = transform.position;

        if (islaCollider != null)
        {
            if (!islaCollider.bounds.Contains(new Vector3(posicionActual.x, islaCollider.bounds.center.y, posicionActual.z)))
            {
                // Si está fuera de los límites, hacemos que caiga al agua
                CaerAlAgua();
            }
        }
    }

    private void CaerAlAgua()
    {
        // Restar una vida al personaje
        vidas--;

        // Verificar si el personaje aún tiene vidas restantes
        if (vidas > 0)
        {
            // Reiniciar la posición del personaje al punto inicial
            transform.position = posicionInicial;

            // También podrías aplicar una animación de caída, sonidos, etc.
            animator.SetTrigger("caer"); // Asegúrate de tener este parámetro en tu animador
        }
        else
        {
            // El personaje ha perdido todas las vidas
            if (gameManager != null)
            {
                gameManager.EndGame();
            }

            // Desactivar el controlador para detener el movimiento
            controller.enabled = false;
            animator.SetBool("caminando", false);
            animator.SetTrigger("gameOver");
            Debug.Log(personaje.ToString() + " ha perdido todas sus vidas!");
        }
    }

    public void RestarVida(int cantidad)
    {
        vidas -= cantidad;
        if (vidas <= 0)
        {
            vidas = 0;
            GameManager.Instance.EndGame();
        }
        else
        {
            // Reiniciar la posición del personaje al punto inicial
            transform.position = posicionInicial;
        }
    }
}








