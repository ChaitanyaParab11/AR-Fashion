using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{

    public SaveData sd;
    public GameObject Door;
    public GameObject OldDoor;

    public float currentTime = 0f;
    public float startingTime = 10f;
    [SerializeField] TextMeshProUGUI countdownText;
    bool stopTime = false;
    public int WinStatus = 0;

    public TextMeshProUGUI GameStatus;
    public AudioSource WinSound1;
    public AudioSource LoseSound1;

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

            //Debug.Log("Time: " + currentTime);

            if (currentTime <= 0)
            {
                //YouLoos();
                currentTime = 0;
                stopTime = true;
                YouFailed();
                YouLoos();
            }

            if (currentTime > 1 && sd.totalCost == 8)
            {
                WinStatus= 1;
                stopTime = true;
            }



            if(WinStatus == 1)
            {
                YouWin();
                stopTime = true;

            }

            if(WinStatus == 2)
            {
                YouEscaped();
                stopTime = true;
            }
            if (WinStatus == 3)
            {
                YouFailed();
                stopTime = true;
            }
        }
        
    }


    void YouWin()
    {
        WinSound1.Play();

        GameStatus.text = "You Win";
        Debug.Log("You Win");
        Invoke("Home", 7);
    }

    void YouLoos()
    {
        LoseSound1.Play();
        GameStatus.text = "You Lose";
        GameStatus.text = "You Lose";
        Debug.Log("--------->You Loss");
        Invoke("Home", 7);
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(2);
    }

    void YouEscaped()
    {

        GameStatus.text = "You Win";
        WinSound1.Play();
        Debug.Log("yOU HAVE ESCAPED");
        OldDoor.SetActive(false);
        Door.SetActive(true);
        Invoke("Home", 7);
    }

    void YouFailed()
    {
        LoseSound1.Play();
        GameStatus.text = "You Lose";
        Debug.Log("yOU LOOSE");
        Invoke("Home", 7);

    }

    void Home()
    {
        SceneManager.LoadScene(0);

    }


}
