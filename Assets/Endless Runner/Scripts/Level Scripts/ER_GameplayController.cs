using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ER_GameplayController : MonoBehaviour
{
    //Variables publicas
    public static ER_GameplayController instance;

    [Header("Arreglos de Objetos")]
    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;

    [Header("Carriles de Objetos")]
    public Transform[] lanes;

    [Header("Tiempos para generar Obstaculos")]
    public float minObstacleDelay = 10f;
    public float maxObstacleDelay = 40f;

    [Header("UI GameObjects")]
    public Text scoreText;
    public GameObject pauseMenu;
    public GameObject victoryMenu;

    [HideInInspector]
    public int score;

    //Variables privadas
    private float halfGroundSize;
    private ER_BaseController baseController;
    public bool isPaused;

    private void Awake()
    {
        //Llamado al metodo que crea el Singleton
        MakeSingleton();
    }

    private void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<ER_GroundBlock>().halfLengh;
        baseController = GameObject.FindGameObjectWithTag("Player").GetComponent<ER_BaseController>();
        isPaused = false;

        StartCoroutine(GenerateObstacleCO());
    }

    //Metodo para crear Singleton
    private void MakeSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    //Corrutina para generar obstaculos
    IEnumerator GenerateObstacleCO()
    {
        //Temporizador local aleatorio basado en la velocidad del jugador
        float _timer = Random.Range(minObstacleDelay, maxObstacleDelay) / baseController.speed.z;
        //Pausa que emplea el temporizador local
        yield return new WaitForSeconds(_timer);

        //---GENERACION DE OBSTACULOS---
        //Llamar a la funcion para generar obstaculos
        CreateObstacles(baseController.gameObject.transform.position.z + halfGroundSize);

        //Iniciar la corutina para generar obstaculos
        StartCoroutine(GenerateObstacleCO());
    }

    //Metodo para generar obstaculos
    private void CreateObstacles(float _zPos)
    {
        //Crear un valor local aleatorio que tenga un valor de 0 a 9
        int _r = Random.Range(0, 10);

        if(0 <= _r && _r < 7)
        {
            //Valor local aleatorio para carril de objetos
            int _objectLane = Random.Range(0, lanes.Length);

            //Llamar a la funcion para agregar obstaculos
            AddObstacle(new Vector3(lanes[_objectLane].transform.position.x, 0f, _zPos),
                        Random.Range(0, obstaclePrefabs.Length));

            //Valor local para carril de zombies
            int _zombieLane = 0;

            //Valores de _objectLane para determinar aparicion de zombies
            if(_objectLane == 0)
            {
                _zombieLane = Random.Range(0, 2) == 1 ? 1 : 2;
            }
            else if(_objectLane == 1)
            {
                _zombieLane = Random.Range(0, 2) == 1 ? 0 : 2;
            }
            else if (_objectLane == 2)
            {
                _zombieLane = Random.Range(0, 2) == 1 ? 1 : 0;
            }

            //Llamar a funcion que añade zombies
            AddZombies(new Vector3(lanes[_zombieLane].transform.position.x, 0.15f, _zPos));
        }
    }

    //Funcion para agregar los obstaculos
    void AddObstacle(Vector3 _pos, int _type)
    {
        GameObject _obstacle = Instantiate(obstaclePrefabs[_type], _pos, Quaternion.identity);
        //Rotar algunos obstaculos que aparezcan
        bool _mirror = Random.Range(0, 2) == 1;

        switch(_type)
        {
            case 0:
                //El objeto se rota -20 en Y si el valor de _mirror es true y 20 en Y si es false
                _obstacle.transform.rotation = Quaternion.Euler(0f, _mirror ? -20 : 20, 0f);
                break;
            case 1:
                //El objeto se rota 20 en Y si el valor de _mirror es true y -20 en Y si es false
                _obstacle.transform.rotation = Quaternion.Euler(0f, _mirror ? 20 : -20, 0f);
                break;
            case 2:
                //El objeto se rota -10 en Y si el valor de _mirror es true y 10 en Y si es false
                _obstacle.transform.rotation = Quaternion.Euler(0f, _mirror ? -10 : 10, 0f);
                break;
            case 3:
                //El objeto se rota -170 en Y si el valor de _mirror es true y 170 en Y si es false
                _obstacle.transform.rotation = Quaternion.Euler(0f, _mirror ? -170 : 170, 0f);
                break;
            default:
                break;
        }

        //Posicion del obstaculo
        _obstacle.transform.position = _pos;
    }

    //Funcion para agregar zombies
    void AddZombies(Vector3 _pos)
    {
        int _count = Random.Range(0, 3) + 1;

        //Ciclo para mover y aparecer zombies
        for(int i = 0; i < _count; i++)
        {
            //Variable local para mover la posicion de la aparicion de zombies
            Vector3 _shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * i);

            //Instanciar Zombies
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], _pos + _shift * i, Quaternion.identity);
        }
    }

    //Funcion para aumentar Score
    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();

        if(score >= 100)
        {
            Time.timeScale = 0;
            victoryMenu.gameObject.SetActive(true);
        }
    }

    //Funcion para pausar el juego
    public void PauseGame()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
    }

    //Funcion para reiniciar el nivel
    public void Reload(string level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    //Funcion para volver al menu principal
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}