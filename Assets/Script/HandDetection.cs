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
    OpenCvSharp.Rect _whiteObject1;
    OpenCvSharp.Rect _whiteObject2;
    const float _maxDistance = 5f; // Maximum distance in meters for white object detection
    const float _minArea = 200f; // Minimum area in pixels for white object detection
    const float _maxAspectRatio = 2.5f; // Maximum aspect ratio of bounding box for white object detection

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

            // Find the two largest contours, which should be the white objects
            double maxArea1 = 0;
            int maxAreaIdx1 = -1;
            double maxArea2 = 0;
            int maxAreaIdx2 = -1;
            for (int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
                if (area > maxArea1)
                {
                    maxArea2 = maxArea1;
                    maxAreaIdx2 = maxAreaIdx1;
                    maxArea1 = area;
                    maxAreaIdx1 = i;
                }
                else if (area > maxArea2)
                {
                    maxArea2 = area;
                    maxAreaIdx2 = i;
                }
            }

            // Draw boxes around the white objects
            if (maxAreaIdx1 != -1 && maxArea1 >= _minArea && maxArea1 / maxArea2 <= _maxAspectRatio)
            {
                // Get the center of the bounding box for the first white object
                _whiteObject1 = Cv2.BoundingRect(contours[maxAreaIdx1]);
                Vector2 center1 = new Vector2(_whiteObject1.X + _whiteObject1.Width / 2, _whiteObject1.Y + _whiteObject1.Height / 2);

                // Check if the white object is under the maximum distance
                if (Vector3.Distance(transform.position, new Vector3(center1.x, center1.y, 0)) <= _maxDistance)
                {
                    // Draw a box around the first white object
                    Cv2.Rectangle(displayFrame, _whiteObject1, new Scalar(0, 255, 0), 2);

                    // Find the second largest white object
                    double maxArea2 = 0;
                    int maxAreaIdx2 = -1;
                    for (int i = 0; i < contours.Length; i++)
                    {
                        if (i != maxAreaIdx1)
                        {
                            double area = Cv2.ContourArea(contours[i]);
                            if (area > maxArea2)
                            {
                                maxArea2 = area;
                                maxAreaIdx2 = i;
                            }
                        }
                    }

                    // Check if there is a second white object and it is under the maximum distance
                    if (maxAreaIdx2 != -1 && Vector3.Distance(transform.position, new Vector3(center1.x, center1.y, 0)) <= _maxDistance)
                    {
                        // Check if the aspect ratio of the second white object is within the maximum limit
                        if (maxArea2 / maxArea1 <= _maxAspectRatio)
                        {
                            // Draw a box around the second white object
                            _whiteObject2 = Cv2.BoundingRect(contours[maxAreaIdx2]);
                            Cv2.Rectangle(displayFrame, _whiteObject2, new Scalar(0, 0, 255), 2);
                        }
                    }
                }
            }

            // Display the Mat in Unity
            Texture newTexture = OpenCvSharp.Unity.MatToTexture(displayFrame);
            GetComponent<Renderer>().material.mainTexture = newTexture;

            // Debug.Log the number of white objects detected
            Debug.Log("Number of white objects detected: " + numWhiteObjects);
        }
    }
}
