using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Camera FPCamera;

    float sinkingSpeed = 2f;
    bool sinking;

    SceneLoader sceneLoader;
    
    private void Start()
    {
        GameObject sceneLoaderAndScore = GameObject.FindWithTag("Finish");
        sceneLoader = sceneLoaderAndScore.GetComponent<SceneLoader>();

        gameOverCanvas.enabled = false;
        sinking = false;
    }

    private void Update()
    {
        if (sinking)
        {
            if (FPCamera.transform.position.y < 1f)
            {
                sinkingSpeed = 0;
            }

            else
            {
                FPCamera.transform.Translate(-Vector3.up * sinkingSpeed * Time.deltaTime);
            }
        }
    }

    public void HandleDeath()
    {
        sinking = true;
        gameOverCanvas.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FindObjectOfType<WeaponSwitcher>().enabled = false;
        FindObjectOfType<CameraController>().enabled = false;
        FindObjectOfType<WeaponSway>().enabled = false;
        FindObjectOfType<VHS.CameraBreathing>().enabled = false;
        FindObjectOfType<Weapon>().enabled = false;
        FindObjectOfType<PlayerHealth>().damageTakenVisual.enabled = false;
        FindObjectOfType<PlayerMovementController>().movementSpeed = 0f;

        sceneLoader.StartDeathSequence();
    }
}
