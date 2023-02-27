using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_Bullet : MonoBehaviour
{
    //Variables privadas
    [SerializeField]
    private Rigidbody RB;

    //Funcion para mover la bala
    public void MoveBullet(float _speed)
    {
        //Adicion de velocidad como fuerza al RB de bala
        //La bala se impulsa hacia adelante
        RB.AddForce(transform.forward.normalized * _speed);
        //Invocar la funcion de destruccion de GOs despues de 5s
        Invoke("DestroyGOs", 5f);
    }

    //Funcion para destruir GameObject
    private void DestroyGOs()
    {
        //Destruir el objeto que tiene este script
        Destroy(gameObject);
    }

    //Metodo para evaluar entradas a colisiones
    private void OnCollisionEnter(Collision _other)
    {
        //Vamos a checar si el objeto entrante cuenta con un tag de Obstaculo
        if(_other.gameObject.tag == "Obstacle")
        {
            //Llamar a la funcion de destruccion de GOs
            DestroyGOs();
        }
    }
}