using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_ZombieScript : MonoBehaviour
{
    //Variables publicas
    [Header("FX de Destruccion")]
    public GameObject bloodFXPrefab;

    [Header("Propiedades del Zombie")]
    public float speed = 2f;

    //Variables privadas
    private Rigidbody RB;
    private bool isAlive;

    private void Start()
    {
        //Dar valor inicial de variables y referencias
        RB = GetComponent<Rigidbody>();
        isAlive = true;
    }

    private void Update()
    {
        //Checar si el zombie esta vivo
        if(isAlive)
        {
            //Si es el caso, el zombie se mueve en direccion en Z al jugador
            RB.velocity = new Vector3(0f, 0f, -speed);
        }

        //Checar si la posicion en Y del zombie es menor a -10
        if(transform.position.y < -10f)
        {
            //Destruir el objeto que tiene este script
            DestroyGameObject();
        }
    }

    //Funcion para cuando mueren los zombies
    void Die()
    {
        //Cambiar el valor del booleano isAlive
        isAlive = false;
        //El zombie no tiene velocidad al morir
        RB.velocity = Vector3.zero;
        //Desactivar el collider del Zombie
        GetComponent<Collider>().enabled = false;
        //Activar la animacion de idle del modelo del zombie
        GetComponentInChildren<Animator>().Play("Idle");
        //Rotar al zombie cuando muere
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        //Nueva escala local del zombie al morir
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        //Nueva posicion del zombie al morir
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    //Metodo para destruir un GO
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    //Funcion para evaluar la entrada a una colision
    private void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.tag == "Bullet" || _other.gameObject.tag == "Player")
        {
            //Instanciar el efecto de muerte
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity);

            //Invocar el metodo de destruccion de GOs
            Invoke("DestroyGameObject", 3f);

            //Subir puntaje del jugador
            ER_GameplayController.instance.AddScore();

            //Llamada a la funcion de muerte del zombie
            Die();
        }
    }
}
