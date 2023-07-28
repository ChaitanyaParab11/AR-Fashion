using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public void Black()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    public void Blue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public void Red()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}
