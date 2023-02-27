using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //Extension para usar DOTween

public class SIU_Platform : MonoBehaviour
{
    //Variables Publicas
    [Header("Arreglo de Picos")]
    [SerializeField]
    private Transform[] spikes; //Arreglo de picos

    [Header("Colectibles")]
    [SerializeField]
    private GameObject coinPrefab; //GO de Moneda

    private bool fallDown; //La plataforma se puede caer? Y/N

    private void Start()
    {
        //Llamar al metodo que activa plataformas
        ActivatePlatform();
    }

    //Funcion para activar picos
    private void ActivateSpikes()
    {
        //Indice local aleatorio
        int _index = Random.Range(0, spikes.Length);

        spikes[_index].gameObject.SetActive(true);

        //Mover el pico usando DOTween, arriba y abajo loopeando
        spikes[_index].DOLocalMoveY(0.7f, 1.3f).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(3f, 5f));
    }

    //Funcion para agregar monedas
    private void AddCoin()
    {
        //Variable local que guarda la instancia de la moneda
        GameObject _coin = Instantiate(coinPrefab);

        //Mover instancia a la posicion de plataforma
        _coin.transform.position = transform.position;

        //Emparentar instancia a este objeto
        _coin.transform.SetParent(transform);

        //Mover moneda en Y usando DOTween
        _coin.transform.DOLocalMoveY(1f, 0f);
    }

    //Metodo que se encarga de activar las plataformas
    private void ActivatePlatform()
    {
        //Entero local aleatorio que va a generar un numero entre 0 y 100
        int _chance = Random.Range(0, 100);

        //Checar si el entero es mayor a 70
        if(_chance > 70)
        {
            //Determinar el tipo de plataforma que se genera
            int _type = Random.Range(0, 8);

            //Condicionales de los cassos de _type
            if(_type == 0 || _type == 5 || _type == 6)
            {
                ActivateSpikes(); ;
            }
            else if(_type == 1 || _type == 4 || _type == 7)
            {
                AddCoin();
            }
            else if(_type == 2)
            {
                fallDown = true;
            }
        }
    }

    //Funcion para caida de plataforma
    private void InvokeFalling()
    {
        //Agregar componente Rigidbody a este objeto
        gameObject.AddComponent<Rigidbody>();
    }

    //Funcion que evalua la entrada a una collision
    private void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.tag == "Player" && fallDown)
        {
            fallDown = false;
            Invoke("InvokeFalling", 2f);
        }
    }
}
