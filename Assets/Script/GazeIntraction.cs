using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GazeIntraction : MonoBehaviour
{
    public Image loadCircle;
    public UnityEvent GVRClick;
    public float totalTime = 1f;
    bool gvrStatus;
    float gvrTimer;

    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            float fillAmount = gvrTimer / totalTime;
            loadCircle.fillAmount = fillAmount;

            if (gvrTimer >= totalTime)
            {
                GVRClick.Invoke();
                //GvrOff();
            }
        }
    }

    public void GvrOn()
    {
        gvrStatus = true;
    }

    public void GvrOff()
    {
        gvrStatus = false;
        gvrTimer = 0f;
        loadCircle.fillAmount = 0f;
    }

}
