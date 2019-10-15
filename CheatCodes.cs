using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    public string[] cheatCode;
    public string[] turnOffCheatCode;
    public int index;
    public bool godMode;

    PlayerHealth playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        
        cheatCode = new string[] { "d", "b", "n", "o", "o", "b" };
        index = 0;
        godMode = false;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(cheatCode[index]))
            {
                index++;
            }

            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }

        if (index == cheatCode.Length)
        {
            // Debug.Log("God Mode Activated!");
            GodMode();
            index = 0;
        }
    }

    public void GodMode()
    {
        godMode = true;
        playerHealth.GodMode();
    }
}
