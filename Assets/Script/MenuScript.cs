using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Quit;
    public GameObject MainMenu;
    public Camera VR_Cam;
    private bool hasIncreasedFOV = false;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingButtonOn()
    {
        Setting.SetActive(true);
    }
    public void SettingButtonOff()
    {
        Setting.SetActive(false);
    }

    public void QuitButton()
    {
        Quit.SetActive(true);
        //MainMenu.SetActive(false);
    }
    public void QuitBack()
    {
        MainMenu.SetActive(true);
        Quit.SetActive(false);     
    }

    public void QuitYes()
    {
        Application.Quit();
    }
    public void ZoomIn()
    {
        if (!hasIncreasedFOV)
        {
            VR_Cam.fieldOfView = 90; // Reduce FOV to zoom in.
            hasIncreasedFOV = true;
        }
        hasIncreasedFOV = false;

    }

    public void ZoomOut()
    {
        if (!hasIncreasedFOV)
        {
            VR_Cam.fieldOfView = 40; // Increase FOV to zoom out.
            hasIncreasedFOV = true;
        }
        hasIncreasedFOV = false;

    }

    public void ResetZoom()
    {
        VR_Cam.fieldOfView = 60;
    }
}
