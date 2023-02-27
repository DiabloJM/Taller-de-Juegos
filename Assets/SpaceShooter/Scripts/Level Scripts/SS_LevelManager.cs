using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_LevelManager : MonoBehaviour
{
    //Variables publicas
    public static SS_LevelManager instance; //Para hacer un singleton

    [Header("Punto de Reaparicion")]
    public GameObject respawnPoint;

    [Header("Particulas")]
    public GameObject deathParticles;
    public GameObject respawnParticles;

    [Header("Tiempo de Reaparicion de Jugador")]
    public float respawnTimer = 1f;

    [Header("Penalizacion por muerte")]
    public int deathPenalty;

    //Variables privadas
    private SS_PlayerController player;

    private void Awake()
    {
        //Llamar al metodo Singleton
        MakeSingleton();
    }

    private void Start()
    {
        player = FindObjectOfType<SS_PlayerController>();
    }

    //Metodo para crear Singleton
    void MakeSingleton()
    {
        //Checar si hay otra instancia de la referencia
        //Checar si la instancia es diferente a nada
        if(instance != null)
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

    //Funcion de reaparicion del Jugador
    public void RespawnPlayer()
    {
        //Llamar a la corrutina de reaparicion
        StartCoroutine(RespawnPlayerCo());
    }

    public IEnumerator RespawnPlayerCo()
    {
        //Instanciar particulas de muerte
        Instantiate(deathParticles, player.transform.position, player.transform.rotation);

        //Desactivar la funcionalidad  y visibilidad del Jugador
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        //Desactivar collider del jugador
        player.GetComponent<Collider2D>().enabled = false;

        //Llamar al metodo TakeLives de SS_LivesManager
        SS_LivesManager.instance.TakeLives();

        //Quitar puntos por haber muerto
        SS_ScoreManager.instance.SubtractPoints(deathPenalty);

        //Cumplir con corrutina. Pausa para reaparecer
        yield return new WaitForSeconds(respawnTimer);

        //Mover al Jugador a la posicion de respawnPoint
        player.transform.position = respawnPoint.transform.position;

        //Activar la funcionalidad  y visibilidad del Jugador
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        //Activar collider del jugador
        player.GetComponent<Collider2D>().enabled = true;

        //Instanciar particulas de reaparicion
        Instantiate(respawnParticles, respawnPoint.transform.position, respawnPoint.transform.rotation);
    }
}