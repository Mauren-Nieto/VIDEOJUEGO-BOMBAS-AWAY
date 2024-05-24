using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public MeshCollider islaCollider;
    public Transform islaTransform;
    public float alturaAgua = -1.0f;

    private Vector3 ultimaPosicionIsla;
    public Vector3 PosicionInicial { get; private set; }

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

        PosicionInicial = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager != null && !gameManager.GameEnded)
        {
            animator.SetBool("caminando", false);

            // Movimiento
            if (personaje == Personaje.Mario)
            {
                if (Input.GetKey(KeyCode.W)) MoveForward();
                if (Input.GetKey(KeyCode.S)) MoveBackward();
                if (Input.GetKey(KeyCode.D)) RotateRight();
                if (Input.GetKey(KeyCode.A)) RotateLeft();
            }
            else if (personaje == Personaje.Luigi)
            {
                if (Input.GetKey(KeyCode.UpArrow)) MoveForward();
                if (Input.GetKey(KeyCode.DownArrow)) MoveBackward();
                if (Input.GetKey(KeyCode.RightArrow)) RotateRight();
                if (Input.GetKey(KeyCode.LeftArrow)) RotateLeft();
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

    private void MoveForward()
    {
        controller.Move(transform.forward * Time.deltaTime * velocidad);
        animator.SetBool("caminando", true);
    }

    private void MoveBackward()
    {
        controller.Move(-transform.forward * Time.deltaTime * velocidad);
        animator.SetBool("caminando", true);
    }

    private void RotateRight()
    {
        transform.Rotate(transform.up, rotacion * Time.deltaTime);
    }

    private void RotateLeft()
    {
        transform.Rotate(transform.up, -rotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("moneda"))
        {
            Destroy(other.gameObject);
            audioSource.Play();
        }
        else if (other.CompareTag("Bullet"))
        {
            other.GetComponent<BulletCollisionHandler>().Explode();
            RestarVida(1);
        }
    }

    private void ChequearLimiteIsla()
    {
        Vector3 posicionActual = transform.position;

        if (islaCollider != null)
        {
            if (!islaCollider.bounds.Contains(new Vector3(posicionActual.x, islaCollider.bounds.center.y, posicionActual.z)))
            {
                CaerAlAgua();
            }
        }
    }

    private void CaerAlAgua()
    {
        vidas--;

        if (vidas > 0)
        {
            transform.position = PosicionInicial;
            animator.SetTrigger("caer");
        }
        else
        {
            if (gameManager != null)
            {
                gameManager.EndGame();
            }

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
            transform.position = PosicionInicial;
        }
    }

    public void RestartPosition()
    {
        transform.position = PosicionInicial;
        vidas = 3;
        controller.enabled = true;
        animator.SetBool("caminando", false);
    }
}





















