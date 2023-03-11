using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class CameraTexture : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    private CascadeClassifier _cascade;
    private OpenCvSharp.Rect _myHand;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();

        // Initialize the CascadeClassifier object
        try
        {
            _cascade = new CascadeClassifier(Application.dataPath + "/hand1.xml");
        }
        catch (OpenCVException e)
        {
            Debug.LogError("Failed to load CascadeClassifier: " + e.Message);
        }
    }

    void Update()
    {
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

        findNewHand(frame);
        display(frame);
    }

    void findNewHand(Mat frame)
    {
        if (_cascade != null)
        {
            var hands = _cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

            if (hands.Length >= 1)
            {
                Debug.Log(hands[0].Location);
                _myHand = hands[0];
            }
        }
    }

    void display(Mat frame)
    {
        if (_myHand != null)
        {
            frame.Rectangle(_myHand, new Scalar(250, 0, 0), 2);
        }

        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newtexture;
    }
}
