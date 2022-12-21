using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 1200;
    public bool timerIsRunning = true;
    public TextMeshProUGUI uiText = null;
    [SerializeField] private UsageCase msgManager;
    [SerializeField] private IngameUI inGameUi;
    bool failedDialog = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 1)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                msgManager.StartDialog(4);
                failedDialog = true;
            }
        }
        if (!timerIsRunning && failedDialog && msgManager.isDialogFinish) {
            inGameUi.OnFailed();
            inGameUi.LockPlayer();
            failedDialog = false;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        uiText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
