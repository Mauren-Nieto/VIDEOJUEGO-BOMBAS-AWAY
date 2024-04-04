using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mario : MonoBehaviour
{
    //Define la velocidad del personaje, se mide en metros por segundo
    //El personaje se mueve a 5 m/s
    public float velocidad = 5.0f;
    //++ (Al ver ++, comentar el funcoinamiento de esta línea)
    public float rotacion = 20.0f;

    //Los siguientes son objetos que representan componentes en el inspector

    //El controlador de personaje, sirve para mover al personaje y que se "choque" con las paredes
    CharacterController controller;
    //Al animador, sirve para animar
    Animator animator;
    //La fuente de sonido, sirve para poner un sonido
    AudioSource audioSource;


    // Este método se ejecuta una sola vez al inicio de cada objeto
    private void Start()
    {
        //Estas líneas se usan para obtener el componente respectivo parausar luego
        //Obtiene el controlador de personaje
        controller = GetComponent<CharacterController>();
        //++
        animator = GetComponent<Animator>();
        //++
        audioSource = GetComponent<AudioSource>();


    }

    // Este método se ejecuta una vez cada cuadro (una vez cada 60vo de segundo)
    private void Update()
    {
        //Establece en falso "caminando" paera que Mario deje de caminar
        animator.SetBool("caminando", false);

        //Si se presiona Arriba
        if(Input.GetKey(KeyCode.UpArrow))
        {
            //Mover el controlador un poco
            controller.Move(transform.forward * Time.deltaTime * velocidad);
            //++
            animator.SetBool("caminando", true);
        }
        
        //++
        if(Input.GetKey(KeyCode.RightArrow))
        {
            //++Rota el objeto segun los grados de rotación
            transform.Rotate(transform.up, rotacion * Time.deltaTime);
        }

        //++
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //++
            transform.Rotate(transform.up, -rotacion * Time.deltaTime);            
        }

        //TAREA: Hacer que mario camine hacia atrás al presoinar la tecla Abajo (DownArrow)

        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "moneda")
        {
            Destroy(other.gameObject);
            audioSource.Play();
        }
    }
    


}
