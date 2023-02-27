using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_ExplosiveObstacle : MonoBehaviour
{
    //Variables publicas
    [Header("FX Obstaculo")]
    public GameObject explosionPrefab;

    [Header("Daño al Jugador")]
    public int damage = 20;

    //Metodo que evalua la entrada de colisiones
    private void OnCollisionEnter(Collision _other)
    {
        //Checar si el objeto entrante es el jugador
        if(_other.gameObject.tag == "Player")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Hacer daño a jugador
            //VOLVER AQUI ---

            //Destruir al GO que tiene este script
            Destroy(gameObject);
        }

        //Checar si el objeto entrante es la bala
        if(_other.gameObject.tag == "Bullet")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            //Destruir al GO que tiene este script
            Destroy(gameObject);
        }
    }
}
