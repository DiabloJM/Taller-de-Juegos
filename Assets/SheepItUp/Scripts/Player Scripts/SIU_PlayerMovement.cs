using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //Extension de DOTween

public class SIU_PlayerMovement : MonoBehaviour
{
    //Variables Privadas
    private Rigidbody RB;

    private float movementForce = 0.5f;
    private float jumpForce = 0.15f;
    private float jumpTime = 0.15f;

    private void Awake()
    {
        //Inicializar REF RB
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Llamar al metodo de inputs del jugador
        GetInput();
    }

    //Metodo de inputs para jugador
    void GetInput()
    {
        //Crear input de dir izquierda
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Llamar a la funcion de salto con parametro true para saltar a la izquierda
            Jump(true);
        }
        //Crear input de dir derecha
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Llamar a la funcion de salto con parametro false para saltar a la derecha
            Jump(false);
        }
    }

    //Funcion para saltar
    void Jump(bool left)
    {
        //Llamada al sonido de salto
        SIU_SoundManager.instance.JumpSound();

        if (left)
        {
            //Brincar a la izquierda
            //Rotar al jugador usando DOTween
            transform.DORotate(new Vector3(0f, 90f, 0f), 0f);

            //Brincar usando DOTween y RB
            RB.DOJump(new Vector3(transform.position.x - movementForce, transform.position.y + jumpForce, transform.position.z), 0.5f, 1, jumpTime);
        }
        else
        {
            //Brincar a la derecha
            //Rotar al jugador usando DOTween
            transform.DORotate(new Vector3(0f, -180f, 0f), 0f);

            //Brincar usando DOTween y RB
            RB.DOJump(new Vector3(transform.position.x, transform.position.y + jumpForce, transform.position.z + movementForce), 0.5f, 1, jumpTime);
        }
    }

}
