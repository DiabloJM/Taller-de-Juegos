using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_EnemySpawner : MonoBehaviour
{
    //Variables publicas
    [Header("Limites en Y")]
    public float minY = -4.22f;
    public float maxY = 4.22f;

    [Header("Asignacion de Enemigos")]
    public GameObject[] asteroidPrefabs;
    public GameObject[] enemyShipPrefabs;

    [Header("Timer de Generacion")]
    public float timer = 2f;
    private void Start()
    {
        //Invocacion de funcion para generar enemigos
        Invoke("SpawnEnemies", timer);
        //Estos aparecen al inicio del juego
    } 
    //Metodo para generar enemigos
    void SpawnEnemies()
    {
        //Vamos a crear una posicion local aleatoria tomando en cuenta los limites de Y
        float _posY = Random.Range(minY, maxY);
        //Valor local que guarda la posicion del generador
        Vector3 _temp = transform.position;
        //Igualar la posicion temporal en Y con la posicion local en Y
        _temp.y = _posY;
        //Vamos a evaluar un rango aleatorio para generar enemigos
        if (Random.Range(0, 2) > 0)
        {
            //Instanciamos un asteroides random del array
            Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], _temp, Quaternion.identity);
        }
        else
        {
            //Instanciamos una nave enemiga
            Instantiate(enemyShipPrefabs[Random.Range(0,enemyShipPrefabs.Length)], _temp, Quaternion.Euler(0f, 0f, 90f));
        }
        //Invocacion de funcion para generar enemigos usando un temporizador
        Invoke("SpawnEnemies", timer);
    }
}