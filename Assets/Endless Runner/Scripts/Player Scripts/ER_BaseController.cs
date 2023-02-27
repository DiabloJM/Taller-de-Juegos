using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_BaseController : MonoBehaviour
{
    //Variables publicas
    [Header("Vector de Velocidad")]
    public Vector3 speed;

    [Header("Valores de Velocidad")]
    public float xSpeed = 8f;
    public float zSpeed = 15f;

    [Header("Features de Movimiento")]
    public float acceleration = 30f;
    public float deceleration = 10f;

    protected float rotationSpeed = 10f;
    protected float maxAngle = 10f;

    [Header("Valored de Pitch de Audio")]
    public float lowSoundPitch;
    public float normalSoundPitch;
    public float highSoundPitch;

    [Header("Clips de Audio")]
    public AudioClip engine_OnSound;
    public AudioClip engine_OffSound;

    //Variables privadas
    private bool isSlow;
    private AudioSource soundManager;
    private void Awake()
    {
        soundManager = GetComponent<AudioSource>();

        speed = new Vector3(0f, 0f, zSpeed);
    }
    protected void MoveLeft()
    {
        speed = new Vector3(-xSpeed / 2f, 0f, speed.z);
    }
    protected void MoveRight()
    {
        speed = new Vector3(xSpeed / 2f, 0f, speed.z);
    }
    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }
    protected void MoveNormal()
    {//Vamos a checar si el jugador va lento
        if(isSlow)
        {
            //Va a dejar de ir lento, cambio de valor al bool isSlow
            isSlow = false;
            //Detener el ausioSource del jugador
            soundManager.Stop();

            //Cambiar el clip del audioSource del Jugador
            soundManager.clip = engine_OnSound;

            //Cambiar el volumen del audioSource
            soundManager.volume = 0.3f;

            //Reproducir audioSource
            soundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, zSpeed);
    }
    protected void MoveSlow()
    {//Vamos a checar si el jugador va NO lento
        if (!isSlow)
        {
            //Va a dejar de ir lento, cambio de valor al bool isSlow
            isSlow = false;
            //Detener el ausioSource del jugador
            soundManager.Stop();

            //Cambiar el clip del audioSource del Jugador
            soundManager.clip = engine_OffSound;

            //Cambiar el volumen del audioSource
            soundManager.volume = 0.3f;

            //Reproducir audioSource
            soundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, deceleration);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, acceleration);
    }
}