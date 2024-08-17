using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeTimer : MonoBehaviour
{
    [SerializeField] public float timeRemaining;
    private bool timerIsRunning = false;
    [SerializeField] private TMP_Text timeText;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameController.Instance.GameOver(0, 0);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);


        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


