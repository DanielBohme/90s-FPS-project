using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    AudioSource audioSource;
    SceneLoader sceneLoader;

    void Start()
    {
        GameObject sceneLoaderAndScore = GameObject.FindWithTag("Finish");
        sceneLoader = sceneLoaderAndScore.GetComponent<SceneLoader>();
        audioSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().buildIndex == 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (sceneLoader.scoreBoard.enabled == true)
        {
            audioSource.Stop();
            Destroy(this.gameObject);
        }
    }

}
