using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool isTransitioning = false;
    public Canvas scoreBoard;
    TimeCounter timeCounter;
    AudioSource audioSource;

    private void Start()
    {
        scoreBoard.enabled = false;
        GetComponent<ScoreBoard>().enabled = false;
        GetComponent<TimeCounter>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartSuccessSequence()
    {
        isTransitioning = true;
        Invoke(nameof(LoadNextLevel), 1f);
        DontDestroyOnLoad(this.gameObject);
        scoreBoard.enabled = true;
        audioSource.Play();
        GetComponent<ScoreBoard>().enabled = true;
    }

    public void StartDeathSequence()
    {
        isTransitioning = true;
        Invoke(nameof(LoadFirstLevel), 3f);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // loop back to start
        }

        SceneManager.LoadScene(nextSceneIndex);
        Time.timeScale = 1;
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}