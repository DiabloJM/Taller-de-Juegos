using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_DestroyGameObjectAfterTime : MonoBehaviour
{
    //Variables publicas
    [Header("Timer de Destrucción de GO")]
    public float timer = 2f;

    private void Start()
    {
        Invoke("DestroyAfterTime", timer);
    }

    //Metodo para destruir GOs
    void DestroyAfterTime()
    {
        //Destruir el GO que tiene este script
        Destroy(gameObject);
    }
}
