using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Bullet : MonoBehaviour
{
    //Variables publicas
    [Header("Velocidad de Bala")]
    public float speed = 6f;

    [Header("Tiempo de Destruccion de Bala")]
    public float destroyTimer = 3f;

    [HideInInspector]
    public bool isEnemyBullet;

    private void Start()
    {
        //Checar si se trata de la bala del enemigo
        if(isEnemyBullet)
        {
            speed *= -1f;
        }
        //Invocar a la funcion de destuccion de bala
        //Esto se activa despues de un tiempo
        Invoke("DestroyGameObject", destroyTimer);
    }
    private void Update()
    {
        //Llamar a la funcion que mueve la bala
        Move();
    }

    //Funcion que mueve la bala
    void Move()
    {
        //Variable temporal que guarda la posicion de bala
        Vector3 _temp = transform.position;
        //Adicion de la posicion temporal en X
        //Disparar de Izquierda a Derecha
        _temp.x += speed * Time.deltaTime;
        //Actualizar la posicion de la bala usando el vector temporal
        transform.position = _temp;
    }

    //Funcion para destruir la bala
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    //Funcion para evaluar entrada de un objeto a un trigger 2D
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.tag == "Bullet" || _other.tag == "Enemy")
        {
            //Llamar a la funcion para destruir la bala
            DestroyGameObject();
        }

        if(_other.tag == "Player" && isEnemyBullet)
        {
            //Si una bala enemiga choca con el jugador este muere y destruye la bala
            SS_LevelManager.instance.RespawnPlayer();
            DestroyGameObject();
        }
    }
}