using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public Image endLevelPortal;
    public SceneLoader sceneLoader;

    private void Start()
    {
        GameObject sceneLoaderAndScore = GameObject.FindWithTag("Finish");
        sceneLoader = sceneLoaderAndScore.GetComponent<SceneLoader>();

        endLevelPortal.enabled = false;
        FindObjectOfType<FaceSystem>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PortalFinish(other);
        }
    }

    public void PortalFinish(Collider player)
    {
        endLevelPortal.enabled = true;

        FindObjectOfType<FaceSystem>().enabled = false;
        FindObjectOfType<WeaponSwitcher>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        sceneLoader.StartSuccessSequence();
    }
}
