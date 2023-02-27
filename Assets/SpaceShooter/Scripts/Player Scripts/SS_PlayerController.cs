using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_PlayerController : MonoBehaviour
{
    //Variables publicas
    [Header("Velocidad de Player")]
    public float speed = 5f;

    [Header("Limites en Y")]
    public float minY;
    public float maxY;

    [Header("Delay de Disparo")]
    public float attackTimer = 0.4f;

    //Variables privadas
    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Transform attackPoint;

    private float currentAttackTimer;
    private bool canAttack;
    private AudioSource laserSound;
    private void Awake()
    {
        laserSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //Declaracion del tiempo de ataque actual
        currentAttackTimer = attackTimer;
    }
    private void Update()
    {
        //Llamar a la funcion que mueve al jugador
        MovePlayer();
        //Llama a la funcion de ataque del jugador
        Attack();
    }

    //Funcion que mueve al jugador
    void MovePlayer()
    {
        //Movimiento en Vertical
        //Checar si el valor del eje vertical es mayor a 0
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            //Crear variable temporal que guarde la posicion del Player
            Vector3 _temp = transform.position;
            //Declarar el valor en Y del vector temporal para ascender
            //Usaremos el valor de speed y el delta para aumentar posicion
            _temp.y += speed * Time.deltaTime;
            //Limitar el movimiento superior en Y
            if (_temp.y > maxY)
                _temp.y = maxY;
            //Asignar la transformacion
            //Actualizar la posicion del jugador usando los valores temporales
            transform.position = _temp;
        }
        else if(Input.GetAxisRaw("Vertical") < 0f)
        {
            //Crear variable temporal que guarde la posicion del Player
            Vector3 _temp = transform.position;
            //Declarar el valor en Y del vector temporal para ascender
            //Usaremos el valor de speed y el delta para disminuir posicion
            _temp.y -= speed * Time.deltaTime;
            //Limitar el movimiento inferior en Y
            if (_temp.y < minY)
                _temp.y = minY;
            //Asignar la transformacion
            //Actualizar la posicion del jugador usando los valores temporales
            transform.position = _temp;
        }
    }

    //Funcion de Ataque
    void Attack()
    {
        //Adicion de tiempo de ataque
        attackTimer += Time.deltaTime;
        //Evaluacion de los tiempos de ataque
        if(attackTimer > currentAttackTimer)
        {
            canAttack = true;
        }
        //Input de disparo
        if(Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            //El jugador ya no puede atacar
            canAttack = false;
            //Se resetea el tiempo de ataque
            attackTimer = 0f;
            //Instanciar la bala
            Instantiate(playerBullet, attackPoint.position, Quaternion.identity);
            //Reproducir Sonido
            laserSound.Play();
        }
    }
}