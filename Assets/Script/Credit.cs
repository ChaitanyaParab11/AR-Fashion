using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credit : MonoBehaviour
{

    private void Start()
    {
        Invoke("Home", 10);
    }

    void Home()
    {
        SceneManager.LoadScene(0);
    }
}
