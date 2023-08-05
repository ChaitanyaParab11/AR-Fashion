using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{

    public SaveData sd;

    public float currentTime = 0f;
    public float startingTime = 10f;
    [SerializeField] TextMeshProUGUI countdownText;
    bool stopTime = false;

    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        if(stopTime == false)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            Debug.Log("Time: " + currentTime);

            if (currentTime <= 0)
            {
                //YouLoos();
                currentTime = 0;
                stopTime = true;
            }

            if (currentTime > 1 && sd.totalCost == 8)
            {
                YouWin();
                stopTime = true;
            }
        }
        
    }


    void YouWin()
    {
        Debug.Log("You Win");
        Invoke("ResetLevel", 7);
    }

    void YouLoos()
    {
        Debug.Log("--------->You Loss");
        Invoke("ResetLevel", 7);
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(2);
    }

    
}
