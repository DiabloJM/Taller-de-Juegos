using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_BGScroll : MonoBehaviour
{
    //Variables publicas
    public float scrollSpeed = 0.1f;

    //Variables privadas
    private MeshRenderer meshRenderer; //Referencia del MeshRenderer del BG
    private float xScroll; //Valor del scolling en x

    private void Awake()
    {
        //Inicializar la referencia meshRenderer
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        //Llamar al metodo para scrollear el BG
        Scroll();
    }

    void Scroll()
    {
        //Determinar el valor de scroll usando la velocidad y el tiempo
        xScroll = Time.time * scrollSpeed;
        //Determinar el desplazamiento al scrollbar
        //Este va a ser un valor local
        Vector2 _offset = new Vector2(xScroll, 0f);
        //Modificar textura usando el offset
        //Aqui pasaremos el nombre generico de la textura y se agrega dicho offset
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", _offset);
    }
}
