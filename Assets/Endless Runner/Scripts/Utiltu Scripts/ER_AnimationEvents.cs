using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_AnimationEvents : MonoBehaviour
{
    //Variables privadas
    private ER_PlayerController playerController;
    private Animator anim;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ER_PlayerController>();
        anim = GetComponent<Animator>();
    }

    //Funcion para resetear el disparo del jugador
    void ResetShooting()
    {
        //Cambiar el valor de canShoot del script ER_PlayerController
        playerController.canShoot = true;
        //Reproducir el estado de "Anim_ShootBar_Idle" del animator
        anim.Play("Anim_ShootBar_Iddle");
    }
}
