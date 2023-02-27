using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_CameraFollow : MonoBehaviour
{
    //Variables publicas
    [Header("Propiedades de Camera")]
    public Transform target;
    public float distance = 6.5f;
    public float height = 3.5f;

    public float heightDamping = 3.25f; //Valor para suavizar la interpolacion linear de altura
    public float rotationDamping = 0.27f;

    private void Start()
    {
        //Inicializar objetivo de camara
        //Buscar en la escena un objetivo con el tag de Player 
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        //Llamar a la funcion de seguimiento al objetivo
        FollowTarget();
    }

    //Funcion de seguimiento al objetivo
    private void FollowTarget()
    {
        //Declarar angulos locales de Objetivo
        float _wantedRotationAngle = target.eulerAngles.y;
        //Declarar altura local de objetivo
        float _wantedHeight = target.position.y + height;
        //Declarar angulos de rotacion locales de camara
        float _currentRotationAngle = transform.eulerAngles.y;
        //Declarar altura local de Camara
        float _currentHeight = transform.position.y;

        //Angulo de rotacion
        //Valor de angulo de rotacion de camara usando una interpolacion lineal con angulos y usamos el damping para suavizarla
        _currentRotationAngle = Mathf.LerpAngle(_currentRotationAngle, _wantedRotationAngle, rotationDamping * Time.deltaTime);
        //Altura
        //Valor de altura de camara usando una interpolacion lineal con angulos y usamos el damping para suavizarla
        _currentHeight = Mathf.Lerp(_currentHeight, _wantedHeight, heightDamping * Time.deltaTime);

        //Angulos de rotacion aplicados a la rotacion
        Quaternion _currentRotation = Quaternion.Euler(0f, _currentRotationAngle, 0f);
        transform.position = target.position;
        transform.position -= _currentRotation * Vector3.forward * distance;

        //Altura de camara
        transform.position = new Vector3(transform.position.x, _currentHeight, transform.position.z);
    }
}
