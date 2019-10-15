using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] AudioClip quitSFX;
    [SerializeField] AudioClip pauseSFX;
    [SerializeField] AudioClip resumeSFX;

    private float quitTime = 3f;
    public Canvas quitGame;
    bool paused = false;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        quitGame.enabled = false;
    }

    private void Update()
    {
        Pause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = togglePause();
        }
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            audioSource.PlayOneShot(resumeSFX);
            FindObjectOfType<CameraController>().enabled = true;
            quitGame.enabled = false;
            Time.timeScale = 1f;
            return (false);
        }

        else
        {
            audioSource.PlayOneShot(pauseSFX);
            FindObjectOfType<CameraController>().enabled = false;
            quitGame.enabled = true;

            if (Input.GetKeyDown(KeyCode.Y))
            {
                StartCoroutine(Quit());
            }

            Time.timeScale = 0f;
            return (true);
        }
    }

    IEnumerator Quit()
    {
        audioSource.PlayOneShot(quitSFX);
        yield return new WaitForSeconds(quitTime);
        Application.Quit();
    }
}
