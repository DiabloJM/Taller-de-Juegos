using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SS_ScoreManager : MonoBehaviour
{
    //Variables publicas
    public static SS_ScoreManager instance;

    [Header("Aspectos de Victoria")]
    public GameObject victoryScreen;
    public string levelToLoad;
    public float waitAfterVictory;

    //Variables privadas
    private Text scoreUI;
    private int scoreCounter = 0;
    private bool[] winLives = { false, false, false };
    private SS_PlayerController player;
    private void Awake()
    {
        //Llamar al metodo Singleton
        MakeSingleton();
    }
    private void Start()
    {
        scoreUI = GetComponent<Text>();
        player = FindObjectOfType<SS_PlayerController>();
    }
    private void Update()
    {
        scoreUI.text = scoreCounter.ToString();

        if(scoreCounter >= 10000 && winLives[0] == false)
        {
            //Agregar una vida
            SS_LivesManager.instance.AddLive();
            winLives[0] = true;
        }

        if (scoreCounter >= 20000 && winLives[1] == false)
        {
            //Agregar una vida
            SS_LivesManager.instance.AddLive();
            winLives[1] = true;
        }

        if (scoreCounter >= 30000 && winLives[2] == false)
        {
            //Agregar una vida
            SS_LivesManager.instance.AddLive();
            winLives[2] = true;

            //Desplegar pantalla de victoria
            victoryScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }

        if(victoryScreen.activeSelf)
        {
            //Reducir tiempo de espera
            waitAfterVictory -= Time.deltaTime;
        }

        if(waitAfterVictory <= 0.0f)
        {
            //Cargar este mismo nivel
            SceneManager.LoadScene(levelToLoad);
        }
    }

    //Funcion para hacer el Singleton
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

    //Funcion para sumar puntos
    public void AddPoints(int points)
    {
        scoreCounter += points;
    }

    //Funcion para restar puntos tras muerte
    public void SubtractPoints(int points)
    {
        scoreCounter -= points;

        if (scoreCounter < 0)
            scoreCounter = 0;
    }
}
