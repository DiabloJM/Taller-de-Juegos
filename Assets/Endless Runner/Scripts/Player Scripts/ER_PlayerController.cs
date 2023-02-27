using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ER_PlayerController : ER_BaseController
{
    //Variables publicas
    [Header("Propiedades de Disparo")]
    public Transform bulletStartPoint;
    public GameObject bulletPrefab;
    public ParticleSystem shootFX;

    [HideInInspector]
    public bool canShoot;

    //Variables privadas
    private Rigidbody RB;
    private Animator shootSliderAnim;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        shootSliderAnim = GameObject.Find("UI_SLD_FireBar").GetComponent<Animator>();

        //Buscar el boton de disparo para usar la funcion ShootingControl en su funcionalidad OnClick
        GameObject.Find("UI_BTN_ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl);

        //El jugador puede disparar cuando inicia el juego
        canShoot = true;
    }
    private void Update()
    {
        //Llamar a la funcion que controla al Jugador con el teclado
        ControlMovementKeyboard();

        //Llamar a funcion para rotar al jugador
        ChangeRotation();

        //Llamar al metodo para disparar
        //ShootingControl();
    }
    private void FixedUpdate()
    {
        //Llamar al metodo que mueve al Jugador
        MovePlayer();
    }
    private void MovePlayer()
    {
        //Vamos a mover al RB de jugador a una posicion especifica
        //Aqui vamos a usar la posicion actual del RB y le vamos a sumar la velocidad
        //Multiplicada por el tiempo

        //Speed es el vector declarado en la clase ER_BaseController
        RB.MovePosition(RB.position + speed * Time.deltaTime);
    }
    private void ControlMovementKeyboard()
    {
        //Vamos a obtener INPUT DE TECLADO
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveFast();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveSlow();
        }
        //Soltar teclas
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            MoveNormal();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            MoveStraight();
        }
    }

    //Metodo para rotar al jugador
    private void ChangeRotation()
    {
        //Checar si la velocidad en X del jugador es mayor a 0
        if(speed.x > 0)
        {
            //Rotar al jugador usando una interpolacion esferica
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        //Checar si la velocidad en X del juagdor es menor a 0
        else if(speed.x < 0)
        {
            //Rotar al jugador usando una interpolacion esferica
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        //Checar si NO hay velocidad en X
        else
        {
            //Rotar al jugador a su rotacion original
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    //Funcion para Disparar
    public void ShootingControl()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            //GO local que guarda la instancia de la bala
            //La bala va a aparecer en el punto de disparo
            GameObject _bullet = Instantiate(bulletPrefab, bulletStartPoint.position, Quaternion.identity);
            //Llamar a la funcion MoveBullet del script de bala
            _bullet.GetComponent<ER_Bullet>().MoveBullet(2000f);
            //Iniciar Efecto de disparo
            shootFX.Play();
        }
        */

        //Checar si la escala de tiempo del juego es diferente a 0
        if(Time.timeScale != 0)
        {
            //Checar si puedo disparar
            if(canShoot)
            {
                //GO local que guarda la instancia de la bala
                //La bala va a aparecer en el punto de disparo
                GameObject _bullet = Instantiate(bulletPrefab, bulletStartPoint.position, Quaternion.identity);

                //Llamar a la funcion MoveBullet del script de bala
                _bullet.GetComponent<ER_Bullet>().MoveBullet(2000f);
                
                //Iniciar Efecto de disparo
                shootFX.Play();

                //Cambiar el valor del booleano canShoot
                //Esto hara que si el jugador ya no pueda disparar
                canShoot = false;

                //Iniciar el estado de animacion de llenado de barra de nuestro slider de disparo
                shootSliderAnim.Play("Anim_ShootBar_FadeIn");
            }
        }
    }
}