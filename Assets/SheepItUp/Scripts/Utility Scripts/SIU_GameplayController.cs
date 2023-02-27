using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SIU_GameplayController : MonoBehaviour
{
    //Variables Publicas
    public static SIU_GameplayController instance;
    public GameObject victoryPanel;

    //Variables Privadas
    [SerializeField]
    private Text scoreText;

    private int score;

    private void Awake()
    {
        if(instance == null)
           instance = this;
    }

    //Funcion para agregar puntaje
    public void AddScore()
    {
        score++;
        //Actualizar el componente de texto de UI
        scoreText.text = "x" + score;
    }

    //Funcion para reiniciar el juego
    public void RestartGame()
    {
        //Llamar a una funcion que carga la escena después de 3 segundos
        Invoke("ReloadScene", 2f);
    }

    //Funcion para cargar la escena de juego
    private void ReloadScene()
    {
        //Cargar la escena de juego usando la extension de SceneManagement
        UnityEngine.SceneManagement.SceneManager.LoadScene("TVJ_JoseFernandoJimenezMichel_SheepItUp");
    }

    //Funcion para volver al menu principal
    public void BackToMainMenu()
    {
        Invoke("MainMenuScene", 2f);
    }

    //Funcion para cargar el menu principal
    private void MainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
