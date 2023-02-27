using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_DestroyGOAfterTime : MonoBehaviour
{
    //Variables publicas
    public float timer = 3f;

    private void Start()
    {
        //Invocar a un metodo de destruccion de GOs
        Invoke("DestroyGameObject", timer);
    }

    //Metodo para destruir GOs
    void DestroyGameObject()
    {
        //Destruir el GOque tiene este script
        Destroy(gameObject);
    }
}
