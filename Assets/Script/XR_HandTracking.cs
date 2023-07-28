using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XR_HandTracking : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public float handDepth = 0.5f;
    public Material cameraMaterial;

    public WebCamTexture backCameraTexture;
    private Color32[] pixels;
    private bool detectHands = true;

    private void Start()
    {
        backCameraTexture = new WebCamTexture();
        cameraMaterial.mainTexture = backCameraTexture;
        backCameraTexture.Play();
    }

    private void Update()
    {
        pixels = backCameraTexture.GetPixels32();

        if (detectHands)
        {
            Vector3 leftPos = FindHand(pixels, true);
            if (leftPos != Vector3.zero)
            {
                leftHand.transform.position = leftPos;
            }

            Vector3 rightPos = FindHand(pixels, false);
            if (rightPos != Vector3.zero)
            {
                rightHand.transform.position = rightPos;
            }
        }
    }

    private Vector3 FindHand(Color32[] pixels, bool isLeft)
    {
        int width = backCameraTexture.width;
        int height = backCameraTexture.height;

        int xStart = isLeft ? 0 : width / 2;
        int xEnd = isLeft ? width / 2 : width;

        int yStart = 0;
        int yEnd = height;

        for (int x = xStart; x < xEnd; x++)
        {
            for (int y = yStart; y < yEnd; y++)
            {
                Color32 pixel = pixels[y * width + x];
                if (isSkin(pixel))
                {
                    Vector3 handPos = new Vector3(x / (float)width, y / (float)height, handDepth);
                    handPos = Camera.main.ViewportToWorldPoint(handPos);
                    return handPos;
                }
            }
        }

        return Vector3.zero;
    }

    private bool isSkin(Color32 color)
    {
        // set color threshold values for skin tones
        int rMin = 95, rMax = 255;
        int gMin = 40, gMax = 255;
        int bMin = 20, bMax = 255;

        // check if the color falls within the threshold values
        return (color.r >= rMin && color.r <= rMax &&
                color.g >= gMin && color.g <= gMax &&
                color.b >= bMin && color.b <= bMax);
    }

}
