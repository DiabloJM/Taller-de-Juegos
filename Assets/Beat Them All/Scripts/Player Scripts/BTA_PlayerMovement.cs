using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_PlayerMovement : MonoBehaviour
{
    //Variables Publicas
    [Header("Velocidad del Jugador")]
    public float walkSpeed = 2f;
    public float zSpeed = 1.5f;

    //Variables Privadas
    private BTA_CharacterAnimation playerAnim;
    private Rigidbody RB;
    private float rotationY = -90f;
    private float rotationSpeed = 15f;

    private void Awake()
    {
        //Dar valor inicial de referencia a RB
        RB = GetComponent<Rigidbody>();
        //Dar valor inicial a la referencia playerAnim
        playerAnim = GetComponentInChildren<BTA_CharacterAnimation>();
    }

    private void Update()
    {
        //Llamada a la funcion que rota al jugador
        RotatePlayer();
        //Llamar a la funcion que anima el caminado del jugador
        AnimatePlayerWalk();
    }

    private void FixedUpdate()
    {
        //Llamada a la función que detecta el movimiento
        DetectMovement();
    }

    //Funcion de deteccion de movimiento
    private void DetectMovement()
    {
        //Manejar la velocidad del RB por medio de Inputs
        //Usaremos ejes de Unity
        //Cambiaremos la velocidad de RB por medio de un nuevo vector que obtenga valor de Inputs en X y Z
        RB.velocity = new Vector3(Input.GetAxisRaw(BTA_Axis.HORIZONTAL_AXIS) * (-walkSpeed),
                                  RB.velocity.y,
                                  Input.GetAxisRaw(BTA_Axis.VERTICAL_AXIS) * (-zSpeed));
    }

    //Funcion para rotar al jugador
    private void RotatePlayer()
    {
        //Checar si el jugador se mueve a la derecha
        if(Input.GetAxisRaw(BTA_Axis.HORIZONTAL_AXIS) > 0f)
        {
            //Rotar al jugador a la derecha
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
        //Si no aplica, checar cuando el jugador va a la izquierda
        else if(Input.GetAxisRaw(BTA_Axis.HORIZONTAL_AXIS) < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY), 0f);
        }
    }

    //Metodo que maneja l animacion de caminado del jugador
    private void AnimatePlayerWalk()
    {
        //Checar si el jugador esta presionando inputs
        //Checar si el jugador esta en movimiento
        if(Input.GetAxisRaw(BTA_Axis.HORIZONTAL_AXIS) != 0 || Input.GetAxisRaw(BTA_Axis.VERTICAL_AXIS) != 0)
        {
            //Llamar al metodo Walk del script de animaciones de personajes
            //Cambiar el parametro Movement a true
            playerAnim.Walk(true);
        }
        else
        {
            //El jugador no se mueve
            //Cambiar el parametro Movement a false
            playerAnim.Walk(false);
        }
    }
}
