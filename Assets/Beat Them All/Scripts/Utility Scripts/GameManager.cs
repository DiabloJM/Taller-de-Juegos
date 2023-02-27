using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variables publias
    [Header("Jugador y Enemigo")]
    public BTA_HealthScript player;
    public BTA_HealthScript enemy;

    [Header("Canvas")]
    public GameObject victoryCanvas;
    public GameObject defeatCanvas;

    private void Update()
    {
        if(player.health <= 0.0f)
        {
            defeatCanvas.gameObject.SetActive(true);
        }

        if(enemy.health <= 0.0f)
        {
            victoryCanvas.gameObject.SetActive(true);
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
