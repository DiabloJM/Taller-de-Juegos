using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Combos
public enum ComboState
{
    //Estados para usar en nuestros combos
    NONE, PUNCH_1, PUNCH_2, PUNCH_3, KICK_1, KICK_2
}

public class BTA_PlayerAttack : MonoBehaviour
{
    //Variables Privadas
    private BTA_CharacterAnimation playerAnim;

    private bool activateTimerToReset;  //Activar el tiempo para resetear combo? Y/N
    private float defaultComboTimer = 0.4f;
    private float currentComboTimer;
    private ComboState currentComboState;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<BTA_CharacterAnimation>();
    }

    private void Start()
    {
        //Dar valor inicial al tiempo actual de combo
        //Este va a ser igial al default al iniciar el juego
        currentComboTimer = defaultComboTimer;
        //Estado inicial del combo
        currentComboState = ComboState.NONE;
    }

    private void Update()
    {
        //Llamar a la funcion para hacer combos
        ComboAttacks();
        //Llamar a la funcion para resetear el estado de los combos
        ResetComboState();
    }

    //Funcion para hacer combos
    private void ComboAttacks()
    {
        //------------GOLPES------------------
        //Input para atacar con el primer golpe
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //Determinar finales de la funcion
            //Determinar salidas de la funcion
            //Vamos a checar si el estado actual es PUNCH_3, KICK_1 o KICK_2
            if(currentComboState == ComboState.PUNCH_3 || currentComboState == ComboState.KICK_1 || currentComboState == ComboState.KICK_2)
            {
                return;
            }

            //Aumentar el estado de combo
            //Movernos del estado NONE a PUNCH_1
            currentComboState++;

            //Activar el booleano para iniciar timer de reseteo de combo
            activateTimerToReset = true;

            //Actualizar el tiempo de combo
            currentComboTimer = defaultComboTimer;

            //Checar si nos encontramos en el estado PUNCH_1
            if(currentComboState == ComboState.PUNCH_1)
            {
                //Llamar al metodo Punch_1 del script de animaciones
                playerAnim.Punch_1();
            }

            //Checar si nos encontramos en el estado PUNCH_2
            if(currentComboState == ComboState.PUNCH_2)
            {
                //Llamar al metodo Punch_2 del script de animaciones
                playerAnim.Punch_2();
            }

            //Checar si nos encontramos en el estado PUNCH_3
            if (currentComboState == ComboState.PUNCH_3)
            {
                //Llamar al metodo Punch_3 del script de animaciones
                playerAnim.Punch_3();
            }
        }

        //------------PATADAS------------------
        //Input para atacar con la primer patada
        if(Input.GetKeyDown(KeyCode.X))
        {
            //Determinar finales de la funcion
            //Determinar salidas de la funcion
            //Vamos a checar si el estado actual es PUNCH_3 o KICK_2
            if(currentComboState == ComboState.PUNCH_3 || currentComboState == ComboState.KICK_2)
            {
                //Si es el caso, salimos de la funcion
                return;
            }

            //Checar si el estado actual de combo es NONE, PUNCH_1 o PUNCH_2
            if(currentComboState == ComboState.NONE || currentComboState == ComboState.PUNCH_1 || currentComboState == ComboState.PUNCH_2)
            {
                //Declarar que el estado actual es el de KICK_1
                currentComboState = ComboState.KICK_1;
            }

            //Checar si nos encontramos en KICK_1
            else if(currentComboState == ComboState.KICK_1)
            {
                //Aumentar el estado de combo, llegar a KICK_2
                currentComboState++;
            }

            //Activar timer para resetear combo
            activateTimerToReset = true;

            //Reiniciar el tiempo para hacer combos
            currentComboTimer = defaultComboTimer;

            //Checar si el combo se encuentra en el estado KICK_1
            if(currentComboState == ComboState.KICK_1)
            {
                //Llamar al metodo Kick_1 del script de animaciones
                playerAnim.Kick_1();
            }

            //Checar si el combo se encuentra en el estado KICK_2
            if (currentComboState == ComboState.KICK_2)
            {
                //Llamar al metodo Kick_2 del script de animaciones
                playerAnim.Kick_2();
            }
        }
    }

    //Funcion para resetear combos
    private void ResetComboState()
    {
        //Checar si esta activado el timer para resetear combos
        if(activateTimerToReset)
        {
            //Reduccion del tiempo actual de combo usando el delta
            currentComboTimer -= Time.deltaTime;

            //Checar si el tiempo actual de combo se ha terminado
            if(currentComboTimer <= 0f)
            {
                //Si es el caso, se nos acaba el combo
                //Regresamos al estado inicial
                currentComboState = ComboState.NONE;

                //Desactivar el tiempo de reseteo
                activateTimerToReset = false;

                //Nuevo valor de tiempo actual de combo
                currentComboTimer = defaultComboTimer;
            }
        }
    }
}