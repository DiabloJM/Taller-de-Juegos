using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIU_SoundManager : MonoBehaviour
{
    //Variables Publicas
    public static SIU_SoundManager instance;

    //Variables Privadas
    [SerializeField]
    private AudioSource gameStart, gameEnd, coinSound, jumpSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //Inicio de juego
    public void GameStartSound()
    {
        gameStart.Play();
    }

    //Fin de juego
    public void GameEndSound()
    {
        gameEnd.Play();
    }

    //Monedas
    public void PickupCoin()
    {
        coinSound.Play();
    }

    //Salto
    public void JumpSound()
    {
        jumpSound.Play();
    }
}
