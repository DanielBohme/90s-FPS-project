using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI hitPointsText;
    [SerializeField] AudioClip takeDamageSFX;
    [SerializeField] AudioClip deathSFX;

    public Image hitPointsBar;
    public Image damageTakenVisual;
    public float damageTakenVisualTime = 0.2f;
    public float maxHitPoints = 100f;

    public bool damaged, godMode, dead;
    public float damageTimeout = 0.5f;
    
    AudioSource audioSource;

    private void Start()
    {
        damaged = false;
        godMode = false;
        dead = false;
        damageTakenVisual.enabled = false;

        UpdatePlayerHealth();
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdatePlayerHealth()
    {
        float ratio = hitPoints / maxHitPoints;
        hitPointsBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        hitPointsText.text = hitPoints.ToString();
    }

    public void TakeDamage(float damage)
    {
        if (godMode == false && dead == false)
        {
            hitPoints -= damage;
            audioSource.Stop();
            audioSource.PlayOneShot(takeDamageSFX);
            StartCoroutine(DamageTakenVisual());

            if (hitPoints <= 0)
            {
                dead = true;
                damaged = false;
                hitPoints = 0;
                GetComponent<DeathHandler>().HandleDeath();
                audioSource.Stop();
                audioSource.PlayOneShot(deathSFX);
            }

            UpdatePlayerHealth();
            StartCoroutine(damageBoolTimer());
        }
    }

    public void GodMode()
    {
        godMode = true;
        hitPoints = maxHitPoints;
        UpdatePlayerHealth();
    }

    private IEnumerator damageBoolTimer()
    {
        damaged = true;
        yield return new WaitForSeconds(damageTimeout);
        damaged = false;
    }

    IEnumerator DamageTakenVisual()
    {
        damageTakenVisual.enabled = true;
        yield return new WaitForSeconds(damageTakenVisualTime);
        damageTakenVisual.enabled = false;
    }

    public void IncreaseHitPoints(float hitPointsAmount)
    {
        hitPoints += hitPointsAmount;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }

        UpdatePlayerHealth();
    }
}
