using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public float damage = 40f;
    [SerializeField] AudioClip attackSFX;

    PlayerHealth target;
    AudioSource audioSource;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        audioSource.Stop();
        audioSource.PlayOneShot(attackSFX);
        // Debug.Log("EnemyAttack");
    }
}
