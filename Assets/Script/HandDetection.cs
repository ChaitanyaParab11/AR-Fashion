using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class HandDetection : MonoBehaviour
{
    WebCamTexture _webCamTexture;
    Mat _frame;
    Scalar _lower;
    Scalar _upper;
    OpenCvSharp.Rect _whiteObject;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();
        _frame = new Mat();
        _lower = new Scalar(200, 200, 200);
        _upper = new Scalar(255, 255, 255);
    }

    void Update()
    {
        if (_webCamTexture.isPlaying && _webCamTexture.didUpdateThisFrame)
        {
            // Convert the WebCamTexture to a Mat
            _frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

            // Create a copy of the original frame to draw bounding boxes
            Mat displayFrame = _frame.Clone();

            // Convert to grayscale
            Cv2.CvtColor(_frame, _frame, ColorConversionCodes.BGR2GRAY);

            // Apply thresholding to isolate white objects
            Cv2.InRange(_frame, _lower, _upper, _frame);

            // Find contours in the thresholded image
            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(_frame, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            // Find the largest contour, which should be the white object
            double maxArea = 0;
            int maxAreaIdx = -1;
            for (int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
                if (area > maxArea)
                {
                    maxArea = area;
                    maxAreaIdx = i;
                }
            }

            // Draw a box around the white object
            if (maxAreaIdx != -1)
            {
                _whiteObject = Cv2.BoundingRect(contours[maxAreaIdx]);
                Cv2.Rectangle(displayFrame, _whiteObject, new Scalar(0, 255, 0), 2);
            }

            // Display the Mat in Unity
            Texture newTexture = OpenCvSharp.Unity.MatToTexture(displayFrame);
            GetComponent<Renderer>().material.mainTexture = newTexture;
        }
    }
}