using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_AbstractScore : MonoBehaviour
{
    //Variables publicas
    public static SS_AbstractScore instance;

    [Header("Sprites de Score")]
    public Sprite S;
    public Sprite A;
    public Sprite B;
    public Sprite C;
    public Sprite D;
    public Sprite E;
    public Sprite F;

    [Header("Texto de Scores")]
    public Text timeText;
    public Text livesText;
    public Text enemiesText;

    [Header("Imagenes de UI")]
    public Image timeImage;
    public Image livesImage;
    public Image enemiesImage;
    public Image averageImage;

    //Variables privadas
    private bool runningTime = true;
    private float timer;
    private int minutes;
    private int seconds;
    private int milliSeconds;
    private int lives;
    private int enemiesNotKill = 0;
    private int timePoints;
    private int livesPoints;
    private int enemiesPoints;
    private int averagePoints;


    private void Awake()
    {
        MakeSingleton();
    }

    private void Update()
    {
        //Sumar tiempo transcurrido
        if(runningTime)
            timer += Time.deltaTime;
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
        }
    }

    public void EnemyScape()
    {
        enemiesNotKill++;
    }

    public void MakeScore()
    {
        //Detener el contador de tiempo
        runningTime = false;

        //Dar puntos al tiempo
        minutes = (int)Mathf.Floor(timer / 60);
        seconds = (int)timer % 60;
        milliSeconds = (int)(timer * 1000);
        milliSeconds = milliSeconds % 1000;

        timeText.text = (minutes < 10 ? $"0{minutes}:" : $"{minutes}:") + (seconds < 10 ? $"0{seconds}:" : $"{seconds}:") + (milliSeconds < 10 ? $"0{milliSeconds}" : $"{milliSeconds}");

        if(timer <= 90f)
        {
            timePoints = 7;
            timeImage.sprite = S;
        }
        else if(timer <= 120f)
        {
            timePoints = 6;
            timeImage.sprite = A;
        }
        else if (timer <= 150f)
        {
            timePoints = 5;
            timeImage.sprite = B;
        }
        else if (timer <= 180f)
        {
            timePoints = 4;
            timeImage.sprite = C;
        }
        else if (timer <= 210f)
        {
            timePoints = 3;
            timeImage.sprite = D;
        }
        else if (timer <= 240f)
        {
            timePoints = 2;
            timeImage.sprite = E;
        }
        else
        {
            timePoints = 1;
            timeImage.sprite = F;
        }

        //Dar puntos a las vidas
        lives = SS_LivesManager.instance.KnowLives();
        livesText.text = lives.ToString();

        switch(lives)
        {
            case 1:
                livesPoints = 1;
                livesImage.sprite = F;
                break;
            case 2:
                livesPoints = 4;
                livesImage.sprite = C;
                break;
            case 3:
                livesPoints = 6;
                livesImage.sprite = A;
                break;
            default:
                livesPoints = 7;
                livesImage.sprite = S;
                break;
        }

        //Dar puntos a los enemigos no eliminados
        enemiesText.text = enemiesNotKill.ToString();

        if(enemiesNotKill == 0)
        {
            enemiesPoints = 7;
            enemiesImage.sprite = S;
        }
        else if(enemiesNotKill == 1 || enemiesNotKill == 2)
        {
            enemiesPoints = 6;
            enemiesImage.sprite = A;
        }
        else if (enemiesNotKill == 3 || enemiesNotKill == 4)
        {
            enemiesPoints = 5;
            enemiesImage.sprite = B;
        }
        else if (enemiesNotKill == 5 || enemiesNotKill == 6)
        {
            enemiesPoints = 4;
            enemiesImage.sprite = C;
        }
        else if (enemiesNotKill == 7 || enemiesNotKill == 8)
        {
            enemiesPoints = 3;
            enemiesImage.sprite = D;
        }
        else if (enemiesNotKill == 9 || enemiesNotKill == 10)
        {
            enemiesPoints = 2;
            enemiesImage.sprite = E;
        }
        else
        {
            enemiesPoints = 1;
            enemiesImage.sprite = F;
        }

        //Hacer el promedio de puntos
        averagePoints = (timePoints + livesPoints + enemiesPoints) / 3;

        switch(averagePoints)
        {
            case 1:
                averageImage.sprite = F;
                break;
            case 2:
                averageImage.sprite = E;
                break;
            case 3:
                averageImage.sprite = D;
                break;
            case 4:
                averageImage.sprite = C;
                break;
            case 5:
                averageImage.sprite = B;
                break;
            case 6:
                averageImage.sprite = A;
                break;
            case 7:
                averageImage.sprite = S;
                break;
            default:
                break;
        }
    }
}
