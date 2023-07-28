using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{

    void Start()
    {
        WebCamTexture webcamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

}
