using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_UniversalAttack : MonoBehaviour
{
    //Variable publicas
    [Header("Layer de Colisiones")]
    public LayerMask collisionLayer; //Layer para impactos/colisiones

    [Header("Propiedades de Detector")]
    public float radius = 1f; //Radio de una esfera creado por fisicas para la deteccion
    public float damage = 2f; //Daño que hace el ataque al impactar objetivo

    [Header("Bools de Identificación")]
    public bool isPlayer; //El jugador tiene este script? Y/N
    public bool isEnemy; //El enemigo tiene este script? Y/N

    [Header("Efecto de Impacto")]
    public GameObject hitFXPrefab; //GO de Efecto de los impactos

    private void Update()
    {
        //Llamada al metodo que detecta colisiones
        DetectCollision();
    }

    //Metodo que detecta colisiones
    void DetectCollision()
    {
        //Arreglo local de colisiones que se llena por medio de una esfera que toca objetos en un layer
        Collider[] _hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        //Checar si hay impactos dentro de nuestro array de collisiones
        if(_hit.Length > 0)
        {
            //Si es el caso, hay impactos en el arreglo

            //Desplegar mensaje en consola para ver que se impacto
            print("We hit the: " + _hit[0].gameObject.name);

            //Vamos a checar si quien ataca es el jugador
            if(isPlayer)
            {
                //POSICIONAR EFECTO DE IMPACTO

                //Variable que guarda la posicion del impacto
                Vector3 _hitPos = _hit[0].transform.position;

                //Posicion del efecto en Y
                _hitPos.y += 1.3f;

                //Posicion del efecto con respecto a un objetivo
                //Checar si el enemigo se encuentra a la derecha
                if (_hit[0].transform.forward.x > 0)
                {
                    _hitPos.x += 0.3f;
                }
                //Cheacr si el enemigo se encuentra volteado a la izquierda
                else if(_hit[0].transform.forward.x < 0)
                {
                    _hitPos.x -= 0.3f;
                }

                //Instanciar el efecto de impacto
                Instantiate(hitFXPrefab, _hitPos, Quaternion.identity);

                //Daño al Enemigo con las opciones de Knockdown
                //Comparacion de tags del GO Jugador
                if(gameObject.CompareTag(BTA_Tags.LEFT_ARM_TAG) || gameObject.CompareTag(BTA_Tags.LEFT_LEG_TAG))
                {
                    //Obtener el script de vida que este asignado al enemigo y este si puede hacer knockdown
                    _hit[0].GetComponent<BTA_HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    //Obtener script de vida de enemigo pero no hecemos knockdown
                    _hit[0].GetComponent<BTA_HealthScript>().ApplyDamage(damage, false);
                }
            }

            //Vamos a checar si quien ataca es el enemigo
            if (isEnemy)
            {
                //POSICIONAR EFECTO DE IMPACTO

                //Variable que guarda la posicion del impacto
                Vector3 _hitPos = _hit[0].transform.position;

                //Posicion del efecto en Y
                _hitPos.y += 1.3f;

                //Posicion del efecto con respecto a un objetivo
                //Checar si el enemigo se encuentra a la derecha
                if (_hit[0].transform.forward.x > 0)
                {
                    _hitPos.x += 0.3f;
                }
                //Cheacr si el enemigo se encuentra volteado a la izquierda
                else if (_hit[0].transform.forward.x < 0)
                {
                    _hitPos.x -= 0.3f;
                }

                //Instanciar el efecto de impacto
                Instantiate(hitFXPrefab, _hitPos, Quaternion.identity);

                _hit[0].GetComponent<BTA_HealthScript>().ApplyDamage(damage, false);
            }

            //Nos aseguramos evitar colisiones continuas para priorizar los eventos de animacion
            gameObject.SetActive(false);
        }
    }
}
