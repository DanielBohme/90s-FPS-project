using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] public float damage = 30f;
    [SerializeField] float fireDelay = 0f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float impactForce;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] public Canvas ammoImage;

    [SerializeField] AudioClip shotgunSFX;
    [SerializeField] AudioClip plasmaRifleSFX;
    [SerializeField] AudioClip plasmaLauncherSFX;
    
    float quadDamageMultiplier = 4f;
    bool canShoot = true;

    public Recoil recoilScript;
    Transform cameraTransform;
    AudioSource audioSource;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }

        // weapon position elasticity
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();

            if (ammoType == AmmoType.Bullets)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(plasmaRifleSFX);
            }
            if (ammoType == AmmoType.Shells)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(shotgunSFX);
            }
            if (ammoType == AmmoType.Rockets)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(plasmaLauncherSFX);
            }

            yield return new WaitForSeconds(fireDelay);
            recoilScript.Fire();

            if (ammoType == AmmoType.Rockets)
            {
                DitzeGames.Effects.CameraShake.ShakeOnce();
            }

            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    public void IncreaseDamage()
    {
        damage *= quadDamageMultiplier;
    }

    public void NormalDamage()
    {
        damage /= quadDamageMultiplier;
    }
}
