using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIU_CameraFollow : MonoBehaviour
{
    //Variables Privadas
    private Transform player;

    private float damping = 2f;
    private float height = 10f;

    private Vector3 startPosition;

    private bool canFollow = true;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        //Declarar posicion inicial de camara
        startPosition = transform.position;
    }

    private void Update()
    {
        Follow();
    }

    //Funcion de seguimiento de camara
    void Follow()
    {
        if (canFollow)
        {
            //Interpolacion lineal entre la posicion inicial de camara respecto a la pos del jugador
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + 10f, player.position.y + height, player.position.z - 10f), Time.deltaTime * damping);
        }
    }

    //Accesor CanFollow
    public bool CanFollow
    {
        get 
        {
            return canFollow;
        }

        set
        {
            canFollow = value;
        }
    }
}
