using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip damagedSFX;

    public float hitPoints = 100f;
    bool isDead = false;

    public GameObject characterShatter;
    AudioSource audioSource;
    KillCounter killCounter;
    GameObject mainCamera;

    private void Start()
    {
        GameObject sceneLoaderAndScore = GameObject.FindWithTag("Finish");
        mainCamera = GameObject.FindWithTag("Player");
        mainCamera.GetComponent<Camera>();
        killCounter = sceneLoaderAndScore.GetComponent<KillCounter>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;

        audioSource.PlayOneShot(damagedSFX);

        if (hitPoints <= 0)
        {
            killCounter.enemiesKilled++;
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);  // audio low on grenade position. use main camera position

        if (isDead) return;
        isDead = true;
        gameObject.SetActive(false);

        var charactershatter = Instantiate(characterShatter, transform.position, transform.rotation);
        
        Destroy(charactershatter, 5f);   // TODO fade out remains
        Destroy(gameObject);
    }
}
