using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerScore;
    private float startTime;

    public bool pressStart = false;
    bool finishCountTime = false;

    public string minutes, seconds;
    private float currentMinutes;

    string scoreString;

    SceneLoader sceneLoader;
    
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        startTheTimer();
        // Debug.Log("time counter start");
    }

    public void startTheTimer()
    {
        startTime = Time.time;
        pressStart = true;
    }

    public void stopTheTimer()
    {
        if (sceneLoader.isTransitioning == true)
        {
            pressStart = false;
            finishCountTime = true;
        }
    }
    
    void Update()
    {
        if (pressStart && sceneLoader.isTransitioning == false)
        {
            float t = Time.time - startTime;
            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f0");
            scoreString = timerScore.text = minutes + ":" + seconds;
        }
    }
}
