using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_LoadScene : MonoBehaviour
{
    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
