using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class HandGestureRecognition : MonoBehaviour
{

    WebCamTexture _webCamTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect MyFace;
    public GameObject one;
    public GameObject one2;
    public GameObject one3;
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();

        cascade = new CascadeClassifier(Application.dataPath + @"haarcascade_frontalface_default.xml");
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().material.mainTexture = _webCamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

        findNewHand(frame);
        display(frame);
    }

    void findNewHand(Mat frame)
    {
        var faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);
        if (faces.Length >= 1)
        {
            MyFace = faces[0];
            one.SetActive(false);
        }
    }

    void display(Mat frame)
    {

        if (MyFace != null)
        {
            frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
            one2.SetActive(false);
        }
        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newtexture;
        one3.SetActive(false);
    }


}
