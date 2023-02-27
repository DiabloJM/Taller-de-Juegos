using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_CharacterAnimationDelegate : MonoBehaviour
{
    //Variables publicas
    public GameObject[] attackPoints;

    public float standUpTimer = 2f;

    //Variables Privadas
    private BTA_CharacterAnimation animationScript;
    private BTA_Enemy_Movement enemyMovement;

    private void Awake()
    {
        animationScript = GetComponent<BTA_CharacterAnimation>();

        if(gameObject.CompareTag(BTA_Tags.ENEMY_TAG))
        {
            enemyMovement = GetComponentInParent<BTA_Enemy_Movement>();
        }
    }

    //Punto Mano Izquierda
    //Funcion para activar punto de ataque
    void LeftArmAttack_ON()
    {
        //Activar el punto de ataque
        attackPoints[0].SetActive(true);
    }

    //Funcion para desactivar punto de ataque
    void LeftArmAttack_OFF()
    {
        //Vamos a checar si el punto de ataque esta activo en la jerarquia
        if (attackPoints[0].activeInHierarchy)
        {
            //Desactivar el punto de ataque
            attackPoints[0].SetActive(false);
        }
    }

    //Punto Mano Derecha
    //Funcion para activar punto de ataque
    void RighttArmAttack_ON()
    {
        //Activar el punto de ataque
        attackPoints[1].SetActive(true);
    }

    //Funcion para desactivar punto de ataque
    void RightArmAttack_OFF()
    {
        //Vamos a checar si el punto de ataque esta activo en la jerarquia
        if (attackPoints[1].activeInHierarchy)
        {
            //Desactivar el punto de ataque
            attackPoints[1].SetActive(false);
        }
    }

    //Punto Pierna Izquierda
    //Funcion para activar punto de ataque
    void LeftLegAttack_ON()
    {
        //Activar el punto de ataque
        attackPoints[2].SetActive(true);
    }

    //Funcion para desactivar punto de ataque
    void LeftLegAttack_OFF()
    {
        //Vamos a checar si el punto de ataque esta activo en la jerarquia
        if (attackPoints[2].activeInHierarchy)
        {
            //Desactivar el punto de ataque
            attackPoints[2].SetActive(false);
        }
    }

    //Punto Pierna Derecha
    //Funcion para activar punto de ataque
    void RightLegAttack_ON()
    {
        //Activar el punto de ataque
        attackPoints[3].SetActive(true);
    }

    //Funcion para desactivar punto de ataque
    void RightLegAttack_OFF()
    {
        //Vamos a checar si el punto de ataque esta activo en la jerarquia
        if (attackPoints[3].activeInHierarchy)
        {
            //Desactivar el punto de ataque
            attackPoints[3].SetActive(false);
        }
    }

    //FUNCIONES DE TAG DEL BRAZO IZQUIERDO
    //Funcion para asignar el tag
    void Tag_LeftArm()
    {
        //Asignar el tag de brazo izquierdo al punto de ataque brazo izquierdo
        attackPoints[0].tag = BTA_Tags.LEFT_ARM_TAG;
    }

    //Funcion para quitar la asignacion del tag
    void Untag_LeftArm()
    {
        //Quitar el tag de brazo izquierdo al punto de ataque brazo izquierdo
        attackPoints[0].tag = BTA_Tags.UNTAGGED_TAG;
    }

    //FUNCIONES DE TAG DEL PIERNA IZQUIERDO
    //Funcion para asignar el tag
    void Tag_LeftLeg()
    {
        //Asignar el tag de brazo izquierdo al punto de ataque brazo izquierdo
        attackPoints[2].tag = BTA_Tags.LEFT_LEG_TAG;
    }

    //Funcion para quitar la asignacion del tag
    void Untag_LeftLeg()
    {
        //Quitar el tag de brazo izquierdo al punto de ataque brazo izquierdo
        attackPoints[2].tag = BTA_Tags.UNTAGGED_TAG;
    }

    //Función para que el enemigo se ponga de pie
    void EnemyStandUp()
    {
        //Llamar a la corutina que levanta al enemigo
        StartCoroutine(StandUpAfterTimeCo());
    }

    //Corutina que levanta al enemigo
    IEnumerator StandUpAfterTimeCo()
    {
        yield return new WaitForSeconds(standUpTimer);

        //Llamar al metodo StandUp del script de animaciones
        //Activar el trigger StandUp del animator del enemigo
        animationScript.StandUp();
    }

    //Funciones para sonidos

    //Funcion para deshabilitar el script de movimiento del enemigo
    void DisableEnemyMovement()
    {
        //Desactivar el componente BTA_EnemyMovement del Enemigo
        enemyMovement.enabled = false;
        //Cambiar el Layer del GO del padre de enemyModel
        transform.parent.gameObject.layer = 0;
    }

    //Funcion para habilitar el script de movimiento del Enemigo
    void EnableEnemyMovement()
    {
        //Activar el componente BTA_EnemyMovement del enemigo
        enemyMovement.enabled = true;
        //Cambiar el Layer del GO del padre de enemyModel
        transform.parent.gameObject.layer = 6;
    }
}