using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //Extension para usar DOTween

public class SIU_LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject startPlatform, endPlatform, platformPrefab; //GO para plataformas

    private float blockWidth = 0.5f; //Ancho de bloque
    private float blockHeight = 0.2f; //Alto de bloque

    [Header("Cantidad de Plataformas a generar")]
    [SerializeField]
    private int amountToSpawn = 100; //Cantidad de bloques a generar
    private int startAmount = 0; //Cantidad inicial de bloques

    private Vector3 lastPosition; //Posicion del bloque generado

    private List<GameObject> spawnedPlatforms = new List<GameObject>(); //Lista de GOs bloques

    [Header("Prefab de Jugador")]
    [SerializeField]
    private GameObject playerPrefab; //GO de Jugador

    private void Awake()
    {
        //Llamar a funcion que genera nivel
        InstantiateLevel();
    }

    //Funcion que genera nivel
    void InstantiateLevel()
    {
        for (int i = startAmount; i < amountToSpawn; i++)
        {
            //Variable local para plataforma nueva
            GameObject newPlatform;

            //Primera Plataforma
            if (i == 0)
            {
                newPlatform = Instantiate(startPlatform);

                //Guardar la posicion de newPlatform dentro de lastPosition
                lastPosition = newPlatform.transform.position;

                //Instanciar al jugador
                //Vector local para guardar lastPosition
                Vector3 temp = lastPosition;
                temp.y += 0.1f;

                Instantiate(playerPrefab, temp, Quaternion.identity);
                //Funcion continua
                continue;
            }
            //Ultima Plataforma
            else if (i == amountToSpawn - 1)
            {
                newPlatform = Instantiate(endPlatform);

                //Asignar tag a la plataforma final
                newPlatform.tag = "EndPlatform";
            }
            //Cualquier otra plataforma de por medio
            else
            {
                newPlatform = Instantiate(platformPrefab);
            }

            //Emparentar las plataformas al objeto que tiene este script
            newPlatform.transform.parent = transform;

            //Agregar plataformas a la lista de GOs
            spawnedPlatforms.Add(newPlatform);

            //Entero local que genera un numero aleatorio
            int left = Random.Range(0, 2);

            if (left == 0)
            {
                //Mover plataforma a la "izquierda"
                newPlatform.transform.position = new Vector3(lastPosition.x - blockWidth, lastPosition.y + blockHeight, lastPosition.z);
            }
            else
            {
                //Mover plataforma a la "derecha"
                newPlatform.transform.position = new Vector3(lastPosition.x, lastPosition.y + blockHeight, lastPosition.z + blockWidth);
            }

            //Actualizar el valor de lastPosition
            lastPosition = newPlatform.transform.position;

            //Animaciones
            //Animar las primeras 25 plataformas
            if(i < 25)
            {
                //Guardar la posicion en Y de plataformas en un valor local
                float endPosY = newPlatform.transform.position.y;

                //Mover la plataforma hacia arriba
                newPlatform.transform.position = new Vector3(newPlatform.transform.position.x, newPlatform.transform.position.y - blockHeight * 3f, newPlatform.transform.position.z);

                //Animar la plataforma usando DOTween
                newPlatform.transform.DOLocalMoveY(endPosY, 0.3f).SetDelay(i * 0.1f);
            }
        }
    }
}
