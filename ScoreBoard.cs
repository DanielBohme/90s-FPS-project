using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killText;
    [SerializeField] TextMeshProUGUI killScore;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI timeScore;
    [SerializeField] TextMeshProUGUI restartText;

    int kills = 0;
    int killsScore;
    float timer = 0;
    int timerScore;

    string minutes, seconds;

    float timeBetweenScores = 1f;

    TimeCounter timeCounter;

    [SerializeField] AudioClip nextLevelSound;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TimeCounter>();
        killText.enabled = false;
        killScore.enabled = false;
        timeText.enabled = false;
        timeScore.enabled = false;
        restartText.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ScoreRoutine());


        //timer += Time.deltaTime;

        //if (timer > 1f)
        //{

        //    timerScore += 1;

        //    //We only need to update the text if the score changed.
        //    timeScore.text = timerScore.ToString();

        //    //minutes = ((int)timer / 60).ToString();
        //    //seconds = (timer % 60).ToString("f2");
        //    //timeScore.text = minutes + ":" + seconds;

        //    //Reset the timer to 0.
        //    timer = 0;
        //}
    }

    IEnumerator ScoreRoutine()
    {
        //int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        //ammoText.text = currentAmmo.ToString();

        yield return new WaitForSeconds(timeBetweenScores);
        killText.enabled = true;
        yield return new WaitForSeconds(timeBetweenScores);
        killScore.enabled = true;
        yield return new WaitForSeconds(timeBetweenScores);
        timeText.enabled = true;
        yield return new WaitForSeconds(timeBetweenScores);
        timeScore.enabled = true;
        yield return new WaitForSeconds(timeBetweenScores);
        restartText.enabled = true;

        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
            AudioSource.PlayClipAtPoint(nextLevelSound, transform.position);
            Destroy(this.gameObject);
        }
    }

    //IEnumerator CountUpToTarget()
    //{
    //    while (currentDisplayScore < targetScore)
    //    {
    //        currentDisplayScore += Time.deltaTime; // or whatever to get the speed you like
    //        currentDisplayScore = Mathf.Clamp(currentDisplayScore, 0f, targetScore);
    //        field.text = currentDisplayScore + "";
    //        yield return null;
    //    }
    //}
}
