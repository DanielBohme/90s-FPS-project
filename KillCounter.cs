using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killScore;

    int numberOfEnemies;
    string scoreString;
    public string counter;
    public int enemiesKilled;
    
    void Start()
    {
        numberOfEnemies = 5;
        enemiesKilled = 0;
    }

    void Update()
    {
        counter = ((enemiesKilled * 100 / numberOfEnemies)).ToString();
        scoreString = killScore.text = counter + "%";
    }
}
