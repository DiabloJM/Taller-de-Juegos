using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_GroundBlock : MonoBehaviour
{
    //Variables publicas
    [Header("Propiedades de Pieza de Terreno")]
    public Transform otherBlock;
    public float halfLengh = 100f;

    //Variables privadas
    private Transform player;
    private float endOffset = 10f;

    private void Start()
    {
        //Dar valor a la REF player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Llamar a la funcion que mueve la pieza de terreno
        MoveBlock();
    }

    //Funcion para mover la pieza de terreno
    private void MoveBlock()
    {
        //Checar si la posicion de Z de la pieza de terreno mas su mitad es
        //menor a la posicion del jugador en Z menos una distancia
        if(transform.position.z + halfLengh < player.transform.position.z - endOffset)
        {
            //El jugador sale de la pieza de terreno y se MUEVE ESTA PIEZA
            //de terreno a una Nueva posicion al FINAL de LA OTRA piza de terreno
            transform.position = new Vector3(otherBlock.position.x, otherBlock.position.y, otherBlock.position.z + halfLengh);
        }
    }
}
