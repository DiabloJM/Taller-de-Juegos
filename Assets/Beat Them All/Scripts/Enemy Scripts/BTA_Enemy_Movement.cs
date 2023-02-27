using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Enemy_Movement : MonoBehaviour
{
    //Variables Publicas
    [Header("Propiedades")]
    public float speed = 2f;
    public float attackDistance = 1.3f;

    //Variables Privadas
    private BTA_CharacterAnimation enemyAnim;
    private Rigidbody RB;
    private Transform playerTarget;

    private float chaseDistanceAfterAttack = 1f;
    private float currentAttackTime;
    private float defaultAttackTime = 2f;

    private bool followPlayer;
    private bool attackPlayer;

    private void Awake()
    {
        enemyAnim = GetComponentInChildren<BTA_CharacterAnimation>();
        RB = GetComponent<Rigidbody>();
        playerTarget = GameObject.FindWithTag(BTA_Tags.PLAYER_TAG).transform;
    }

    private void Start()
    {
        //El enemigo puede seguir al jugador al inicio del juego
        followPlayer = true;

        //Iniciar el tiempo actual de ataques usando el valor default
        currentAttackTime = defaultAttackTime;
    }

    private void Update()
    {
        //Llamar a la funcion de ataques del enemigo
        EnemyAttack();
    }

    private void FixedUpdate()
    {
        //Llamar a la funcion para seguir al objetivo
        FollowTarget();
    }

    //Funcion para que el enemigo siga su objetivo
    private void FollowTarget()
    {
        //Checar si el enemigo NO puede seguir al objetivo
        if(!followPlayer)
        {
            //Salir de la funcion
            return;
        }

        //Lo que sigue se hace si el enemigo SI puede seguir al objetivo
        //Evaluar si la distancia entre el enemigo y el objetivo es mayor que la distancia de ataque
        if(Vector3.Distance(transform.position, playerTarget.position) > attackDistance)
        {
            //Si es el caso, el enemigo sigue al jugador
            //Rotar al enemigo hacia el objetivo
            transform.LookAt(playerTarget);

            //Modificar la velocidad de RB del enemigo para ir hacia adelante
            RB.velocity = transform.forward * speed;

            //Validacion de movimiento del enemigo
            //Checar si la magnitud de la velocidad de RB es diferente de 0
            //Checar si el enemigo se mueve
            if(RB.velocity.sqrMagnitude != 0)
            {
                //Existe movimiento, animar el caminado de enemigo
                //Usar el metodo Walk del script de animaciones
                enemyAnim.Walk(true);
            }
        }
        //Checar si la distancia entre el enemigo y el jugador es menor o igual a la distancia de ataque
        else if(Vector3.Distance(transform.position,playerTarget.position) <= attackDistance)
        {
            //El enemigo esta en rango para atacar
            //Detener la velocidad del RB del enemigo
            RB.velocity = Vector3.zero;

            //Detener la animacion de caminado del enemigo 
            enemyAnim.Walk(false);

            //El enemigo deja de seguir al objetivo
            followPlayer = false;

            //El enemigo puede atacar al jugador
            attackPlayer = true;
        }
    }

    //Metodo de ataque del enemigo
    private void EnemyAttack()
    {
        //Checar si el enemigo NO puede atacar
        if(!attackPlayer)
        {
            return;
        }

        //Todo se hace si el enemigo SI puede atacar al jugador
        //Aumentar el tiempo de ataque actual del ataque de enemigo usando el tiempo
        currentAttackTime += Time.deltaTime;

        //Checar si el tiempo actual de ataque es mayor al tiempo default de ataque
        if(currentAttackTime > defaultAttackTime)
        {
            //El enemigo ataca con un ataque random al jugador
            //Usar la funcion de EnemyAttack del script de animaciones usando un random como parametro
            enemyAnim.EnemyAttack(Random.Range(0, 3));

            //Actualización de tiempo de ataque
            //Declarar tiempo actual de ataque en 0 para que vuelva a aumentar
            currentAttackTime = 0f;
        }

        //Evaluar si la distancia entre el enemigo y el objetivo es mayor a la distancia de ataque más la de persecucion
        if(Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chaseDistanceAfterAttack)
        {
            //Si es el caso, el enemigo deja de atacar
            attackPlayer = false;

            //El enemigo sigue al objetivo
            followPlayer = true;
        }
    }
}
