using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIU_PlayerInteraction : MonoBehaviour
{
    //Variables Privadas
    private Rigidbody RB;
    private bool playerDied;
    private SIU_CameraFollow cameraFollow;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        cameraFollow = Camera.main.GetComponent<SIU_CameraFollow>();
    }

    private void Update()
    {
        //Vamos a checar si la velocidad del Rigidbody del jugador cuenta con una magnitud alta
        if(!playerDied && RB.velocity.sqrMagnitude > 60)
        {
            //Esto significa que el jugador esta cayendo
            playerDied = true;

            //La camara deja de seguir al jugador
            cameraFollow.CanFollow = false;

            //Sonido de fin de juego
            SIU_SoundManager.instance.GameEndSound();

            //Reiniciar el juego
            SIU_GameplayController.instance.RestartGame();
        }
    }

    //Funcion que evalua entradas a un trigger
    private void OnTriggerEnter(Collider _other)
    {
        //Interaccion con GO que tienen trigger
        //Moneda
        if (_other.tag == "Coin")
        {
            //Sonido de colectible y subir score
            SIU_SoundManager.instance.PickupCoin();
            SIU_GameplayController.instance.AddScore();

            //Destruir el objeto entrante
            Destroy(_other.gameObject);
        }

        //Picos
        if(_other.tag == "Spike")
        {
            //Camara deja de seguir al jugador
            cameraFollow.CanFollow = false;

            //Sonido de fin de juego
            SIU_SoundManager.instance.GameEndSound();

            //Reiniciar el juego
            SIU_GameplayController.instance.RestartGame();

            //Destruir GO de Jugador
            Destroy(gameObject);
        }
    }

    //Funcion para evaluar entrada a colisiones
    private void OnCollisionEnter(Collision _other)
    {
        //Plataforma final
        if(_other.gameObject.tag == "EndPlatform")
        {
            //GANAMOS!
            //Sonido de Victoria
            SIU_SoundManager.instance.GameStartSound();

            SIU_GameplayController.instance.victoryPanel.SetActive(true);

            //Reiniciar el juego
            SIU_GameplayController.instance.BackToMainMenu();
        }
    }
}
