using UnityEngine;
using OpenCvSharp;

public class HandRecog : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    private CascadeClassifier _cascadeClassifier;
    private Texture2D _handTexture;
    private const int Width = 640;
    private const int Height = 480;


    void Start()
    {

        // Initialize a new WebCamTexture
        var devices = WebCamTexture.devices;
        _webCamTexture = new WebCamTexture(devices[0].name, Width, Height);
        _webCamTexture.Play();

        // Load the pre-trained hand detection cascade classifier
        string cascadePath = Application.dataPath + "/HaarCascadeClassifiers/haarcascade_hand.xml";
        _cascadeClassifier = new CascadeClassifier(cascadePath);

        if (_cascadeClassifier == null)
        {
            Debug.LogError("Failed to load cascade classifier on Start Function()");
        }
    }


    void Update()
    {
        
        // Convert the current camera frame to an OpenCVsharp Mat
        var imageMat = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

        // Detect the hand in the Mat using the cascade classifier
        var handRect = DetectHand(imageMat);

        // Draw a green box around the detected hand
        if (handRect != null)
        {
            Cv2.Rectangle(imageMat, handRect.Value, new Scalar(0, 255, 0), 2);
        }

        // Convert the Mat to a texture for display
        _handTexture = new Texture2D(imageMat.Width, imageMat.Height);
        WebCamDevice[] devices = WebCamTexture.devices;
        OpenCvSharp.Unity.MatToTexture(imageMat, _handTexture);

        // Display the hand texture on the object that this script is attached to
        GetComponent<Renderer>().material.mainTexture = _handTexture;
    }

    private OpenCvSharp.Rect? DetectHand(Mat imageMat)
    {
        // Convert the Mat to grayscale
        var grayMat = new Mat();
        Cv2.CvtColor(imageMat, grayMat, ColorConversionCodes.BGR2GRAY);

        // Check if the cascade classifier object is null
        if (_cascadeClassifier == null)
        {
            Debug.LogError("CascadeClassifier object is null.");
            return null;
        }

        // Use the cascade classifier to detect the hand
        var hands = _cascadeClassifier.DetectMultiScale(grayMat, 1.1, 3, HaarDetectionType.DoCannyPruning, new Size(30, 30));

        // Return the bounding box of the detected hand, or null if no hand was detected
        if (hands.Length > 0)
        {
            return hands[0];
        }
        else
        {
            return null;
        }
    }

}