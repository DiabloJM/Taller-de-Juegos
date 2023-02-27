using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_DestroyParticles : MonoBehaviour
{
    //Variable privada
    private ParticleSystem thisParticleSystem;
    private void Start()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //Llamar a la funcion para destruir particulas
        DestroyParticles();
    }

    //Funcion para destruir particulas
    void DestroyParticles()
    {
        //Checar si el emisor de particulas esta en reproduccion
        if (thisParticleSystem.isPlaying)
            return;

        Destroy(gameObject);
    }
}