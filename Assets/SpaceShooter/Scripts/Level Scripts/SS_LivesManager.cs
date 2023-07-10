using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SS_LivesManager : MonoBehaviour
{
    //Variables publicas
    public static SS_LivesManager instance;

    [Header("Vidas de Jugador")]
    public int startingLives;

    [Header("Aspectos de GameOver")]
    public GameObject gameOverScreen;
    public string levelToLoad;
    public float waitAfterGameOver;

    [Header("Jugador")]
    public SS_PlayerController player;

    //Variables privadas
    private int livesCounter;
    private Text livesText;
    private void Awake()
    {
        //Llamar al metodo Singleton
        MakeSingleton();
    }
    private void Start()
    {
        //Inicializar la REF livesText
        livesText = GetComponent<Text>();
        //Inicializar la REF del jugaddor
        player = FindObjectOfType<SS_PlayerController>();
        //Determinar el valor del contador de vidas
        livesCounter = startingLives;
    }
    void MakeSingleton()
    {
        //Checar si hay otra instancia de la referencia
        //Checar si la instancia es diferente a nada
        if (instance != null)
        {
            //Si existe otra instancia, destruimos este objeto
            Destroy(gameObject);
        }
        else
        {
            //La instancia referencia a esta clase
            instance = this;

            //Evitar la destruccion de este GO al cambiar de escena
            //DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        //Condicion para que aparezca la pantalla de GameOver
        //La pantalla va a aparecer cuando el jugador muere DESPUES de la vida 0
        if(livesCounter <= 0)
        {
            //Activar la pantalla de GameOver
            gameOverScreen.SetActive(true);
            //Desactivar el GO del Jugador
            player.gameObject.SetActive(false);
        }
        //Cambiar el contenido del componente de Texto
        //Aqui vamos a desplegar la "X" y la cantidad de vidas que tenga el contador
        livesText.text = "X" + livesCounter;
        //Checar si la pantalla de GameOver esta activa en escena
        if(gameOverScreen.activeSelf)
        {
            //Reducir el tiempo de espera usando el tiempo
            waitAfterGameOver -= Time.deltaTime;
        }
        //Evaluar si se termino el tiempo de espera
        if(waitAfterGameOver <= 0)
        {
            //Cargar la escena escrita en el string
            SceneManager.LoadScene(levelToLoad);
        }
    }

    //Metodo para perder vidas
    public void TakeLives()
    {
        //Disminuir el contador de vidas
        livesCounter--;
    }

    //Metodo para agregar vidas
    public void AddLive()
    {
        //Aumentar el contador de vidas
        livesCounter++;
    }

    //Metodo para saber las vidas actuales
    public int KnowLives()
    {
        return livesCounter;
    }
}