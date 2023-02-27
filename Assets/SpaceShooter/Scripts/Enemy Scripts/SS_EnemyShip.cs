using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_EnemyShip : MonoBehaviour
{
    //Varaibles publicas
    [Header("Velocidades de Enemigo")]
    public float speed = 5f;
    public float rotateSpeed = 50f;

    [Header("Propiedades de Enemigo")]
    public bool canShoot;
    public bool canRotate;

    public bool canMove = true;

    [Header("Limites de Enemigo")]
    public float boundX = -10f;

    [Header("Asignaciones")]
    public Transform[] attackPoints;
    public GameObject bulletPrefab;

    //Variables privadas
    private Animator anim;
    private AudioSource explosionSound;

    private void Awake()
    {
        //Inicializar referencias
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //Vamos a checar si el enemigo puede rotar
        if(canRotate)
        {
            //Valor de velocidad de rotación
            rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);

            //Randomizar una rotacion
            if (Random.Range(0,2) > 0)
            {
                //Cambio de sentido a la rotacion
                rotateSpeed *= -1f;
            }
        }
        //Checar si el enemigo puede disparar
        if(canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }
    private void Update()
    {
        //Llamada al metodo de movimiento de los enemigos
        MoveEnemy();
        //Llamada al metodo de rotaciones de asteroides
        RotateEnemy();
    }
    void MoveEnemy()
    {
        //Checar si el enemigo se puede mover
        if(canMove)
        {
            //Variable que guarda la posicion actual del enemigo
            Vector3 _temp = transform.position;
            //Decremento de la pos x del enemigo usando el tiempo
            _temp.x -= speed * Time.deltaTime;
            //Actualizar la posicion de este objeto usando el vector temp
            transform.position = _temp;
            //Validar el limite de -x de la posicion del enemigo
            //Checar si el valor del _temp en x es menor al valor del limite en x
            if(_temp.x < boundX)
            {
                //Destruir al gameObject que tiebe este script
                Destroy(gameObject);
            }
        }
    }
    //Metodo para rotar enemigos
    void RotateEnemy()
    {
        //Checar si el enemigo puede rotar
        if(canRotate)
        {
            //Aplicar la rrotacion con respecto al mundo
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.World);
        }
    }
    //Metodo de disparo
    void StartShooting()
    {
        for(int i=0; i<attackPoints.Length; i++)
        {
            //Guardar la instancia de la bala dentro de una variable
            GameObject _bullet = Instantiate(bulletPrefab, attackPoints[i].position, Quaternion.identity);

            //Acceder a la variable isEnemyBullet de SS_Bullet para cambiar la direccion de disparo
            //Vamos a acceder al componente SS_Bullet de la bala para cambiar valor de direccion
            _bullet.GetComponent<SS_Bullet>().isEnemyBullet = true;
        }

        //Checar si el enemigo puede disparar
        if(canShoot)
        {
            //Si es el caso, se invoca este metodo usando un tiempo aleatorio de 1 a 3s
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    //Funcion para destruir GameObjects
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    //Metodo que evalua la entrada de un objeto a un trigger 2D
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.tag == "Bullet")
        {
            canMove = false;

            if(canShoot)
            {
                canShoot = false;
                CancelInvoke("StartShooting");
                SS_ScoreManager.instance.AddPoints(500);
            }
            //Desactivar el collider
            GetComponent<Collider2D>().enabled = false;

            Invoke("DestroyGameObject", 0.5f);
            //Reproducir sonido de explosion
            explosionSound.Play();
            //Incrementar Score
            SS_ScoreManager.instance.AddPoints(500);
            //Animacion de destruccion del enemigo
            anim.Play("Anim_SS_Enemy_Destroy");
        }
        //Checar si el objeto entrante es el Jugaddor
        if(_other.tag == "Player")
        {
            //Llamar a la funcion de Reaparicion de Jugador usando el Singleton dde SS_LevelManager
            SS_LevelManager.instance.RespawnPlayer();
        }
    }
}