using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGameObject : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject turret;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private float projectileLaunchForce;
    [SerializeField] private float launchRechargeTime;
    [SerializeField] private float launchDuration;
    [SerializeField] private float turretStationaryTime;
    [SerializeField] AudioClip launchSFX;

    AudioSource audioSource;

    private Vector3 startPos;
    private Vector3 endPos;

    private float waitTime = 0.5f;
    private float distance = 5f;
    private float lerpTime = 3f;
    private float currentLerpTime = 0f;

    private int maxGrenadeLaunches = 1;
    private float currentLaunchRechargeTime = 0f;
    private int remainingGrenadeLaunches = 1;

    float timeLeft;
    public Image timerBar;

    private void Start()
    {
        startPos = turret.transform.localPosition;
        endPos = turret.transform.localPosition + Vector3.forward * distance;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(MoveTurret());
        }

        if (remainingGrenadeLaunches < maxGrenadeLaunches)
        {
            currentLaunchRechargeTime += Time.deltaTime;

            if (currentLaunchRechargeTime >= launchRechargeTime)
            {
                remainingGrenadeLaunches++;
                currentLaunchRechargeTime = 0f;
            }
        }
    }

    IEnumerator MoveTurret()
    {
        if (remainingGrenadeLaunches <= 0)
        {
            yield break;
        }

        remainingGrenadeLaunches--;

        float currentTime = 0.0f;
        float normalizedDelta;
        Vector3 startPos = turret.transform.localPosition;
        Vector3 endPos = turret.transform.localPosition + new Vector3(0f,0f,0.5f);

        while (currentTime < 0.2f)
        {
            currentTime += Time.deltaTime;
            normalizedDelta = currentTime / 0.2f;
            turret.transform.localPosition = Vector3.Lerp(startPos, endPos, normalizedDelta);
            yield return null;
        }

        yield return new WaitForSeconds(turretStationaryTime);

        StartCoroutine(Timer());
        muzzleFlash.Play();
        LaunchProjectile();
        currentTime = 0.0f;

        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            normalizedDelta = currentTime / 0.5f;
            turret.transform.localPosition = Vector3.Lerp(endPos, startPos, normalizedDelta);
            yield return null;
        }

        yield return new WaitForSeconds(launchDuration);
    }

    private void LaunchProjectile()
    {
        audioSource.PlayOneShot(launchSFX);
        GameObject projectileInstance = Instantiate(
            projectilePrefab, 
            projectileSpawnTransform.position, 
            Quaternion.LookRotation(Vector3.up));

        projectileInstance.GetComponent<Rigidbody>().AddForce(projectileSpawnTransform.forward * projectileLaunchForce, ForceMode.VelocityChange);
    }

    IEnumerator Timer()
    {
        while (timeLeft < launchRechargeTime)
        {
            timeLeft += Time.deltaTime;
            float ratio = timeLeft / launchRechargeTime;
            timerBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
            yield return null;
        }

        timeLeft = 0;
    }
}
