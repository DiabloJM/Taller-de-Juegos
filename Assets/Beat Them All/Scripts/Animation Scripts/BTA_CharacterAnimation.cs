using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_CharacterAnimation : MonoBehaviour
{
    //Variables Privadas
    private Animator anim;

    private void Awake()
    {
        //Dar valor a referencia anim
        anim = GetComponent<Animator>();
    }

    //Metodo para animacion de movimiento
    public void Walk(bool _canMove)
    {
        //Cambiar el valor del parametro Movement de Animator usando el valor del parametro local
        anim.SetBool(BTA_AnimationTags.MOVEMENT, _canMove);
    }

    //Metodo para animacion de Golpe 1
    public void Punch_1()
    {
        //Activar el trigger del parametro Punch1 de Animator
        anim.SetTrigger(BTA_AnimationTags.PUNCH_1_TRIGGER);
    }

    //Metodo para animacion de Golpe 2
    public void Punch_2()
    {
        //Activar el trigger del parametro Punch2 de Animator
        anim.SetTrigger(BTA_AnimationTags.PUNCH_2_TRIGGER);
    }

    //Metodo para animacion de Golpe 3
    public void Punch_3()
    {
        //Activar el trigger del parametro Punch3 de Animator
        anim.SetTrigger(BTA_AnimationTags.PUNCH_3_TRIGGER);
    }

    //Metodo para animacion de Patada 1
    public void Kick_1()
    {
        //Activar el trigger del parametro Kick1 de Animator
        anim.SetTrigger(BTA_AnimationTags.KICK_1_TRIGGER);
    }

    //Metodo para animacion de Patada 2
    public void Kick_2()
    {
        //Activar el trigger del parametro Kick2 de Animator
        anim.SetTrigger(BTA_AnimationTags.KICK_2_TRIGGER);
    }

    //Metodo para animar ataques del Enemigo
    public void EnemyAttack(int _attack)
    {
        //Activar los triggers de ataque del enemigo usando el valor del int local
        switch(_attack)
        {
            case 0:
                anim.SetTrigger(BTA_AnimationTags.ATTACK_1_TRIGGER);
                break;
            case 1:
                anim.SetTrigger(BTA_AnimationTags.ATTACK_2_TRIGGER);
                break;
            case 2:
                anim.SetTrigger(BTA_AnimationTags.ATTACK_3_TRIGGER);
                break;
            default:
                break;
        }
    }

    //Metodo que reproduce animacion de Idle
    public void IdleAnimation()
    {
        //Reproducir animacion de Idle de Animator
        anim.Play(BTA_AnimationTags.IDLE_ANIMATION);
    }

    //Metodo para animar el noqueo del enemigo
    public void Knockdown()
    {
        //Activar trigger de Knockdown del animator enemigo
        anim.SetTrigger(BTA_AnimationTags.KNOCKDOWN_TRIGGER);
    }

    //Metodo para animar cuando el enemigo se levanta
    public void StandUp()
    {
        //Activar trigger de StandUp del animator enemigo
        anim.SetTrigger(BTA_AnimationTags.STAND_UP_TRIGGER);
    }

    //Metodo para animar cuando el enemigo es golpeado
    public void Hit()
    {
        //Activar trigger de Hit del animator enemigo
        anim.SetTrigger(BTA_AnimationTags.HIT_TRIGGER);
    }

    //Metodo para animar la muerte
    public void Death()
    {
        //Activar trigger de Death del animator
        anim.SetTrigger(BTA_AnimationTags.DEATH_TRIGGER);
    }
}
