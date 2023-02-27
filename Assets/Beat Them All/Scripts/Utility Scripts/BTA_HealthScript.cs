using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_HealthScript : MonoBehaviour
{
    //Variables publicas
    public float health = 100f;

    public bool isPlayer;

    public BTA_Enemy_Movement enemyMovement;

    //Variables privadas
    private BTA_CharacterAnimation animationScript;

    private bool characterDied;

    private void Awake()
    {
        animationScript = GetComponentInChildren<BTA_CharacterAnimation>();
    }

    //Metodo para aplicar daño
    public void ApplyDamage(float _damage, bool _canKnockdown)
    {
        //Checar si el personaje esta muerto
        if(characterDied)
        {
            return;
        }

        //Esto es si el personaje NO esta muerto

        //Reducir los puntos de vida usando el parametro flotante local
        health -= _damage;

        //Actualizar barra de vida

        //Condicional de muerte del personaje
        //Vamos a checar si los puntos de vida son 0 o menor
        if(health <= 0f)
        {
            //Llamar al metodo Death del script de animaciones
            //Cambiar el parametro Death de los animator
            animationScript.Death();
            //El personaje esta muerto
            characterDied = true;
            //Checar si el jugador tiene este script
            if(isPlayer)
            {
                //Desactivar el script de movimiento del enemigo
                enemyMovement.enabled = false;
            }

            return;
        }

        //Checar si el jugador NO tiene este script
        if(!isPlayer)
        {
            //El enemigo tiene este script

            //Checar si el valor parametro _canKnockdown es true
            if(_canKnockdown)
            {
                //Si es el caso, vamos a tener un valor random para llamar a la animacion de noqueo del enemigo
                //Existe un 50% de oportunidad de noquear al enemigo usando ciertos ataques
                if(Random.Range(0,2) > 0)
                {
                    //Llamar al metodo Knockdown del script de animaciones
                    //Activar el trigger Knockdown del animator del enemigo
                    animationScript.Knockdown();
                }
            }
            else
            {
                //Esto pasa cuando el parametro es falso

                //Crear un valor aleatorio para llamar con cierta probabilidad a la animacion de impacto
                //Existe un 33% de oportunidad para llamar a la animacion de impacto
                if(Random.Range(0,3) > 1)
                {
                    //Llamar a la animacion de impacto del enemigo
                    //Activando el trigger Hit del animator del enemigo
                    animationScript.Hit();
                }
            }
        }
    }
}
