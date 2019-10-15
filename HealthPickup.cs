using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] RawImage healthPickupVisual;
    [SerializeField] AudioClip healthPSFX;

    public float hitPointsAmount = 30f;
    public float pickupVisualTime = 0.2f;

    AudioSource audioSource;

    private void Start()
    {
        healthPickupVisual.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (FindObjectOfType<PlayerHealth>().hitPoints == FindObjectOfType<PlayerHealth>().maxHitPoints)
            {
                return;
            }

            else
            {
                Pickup(other);
            }
        }
    }

    public void Pickup(Collider player)
    {
        audioSource.PlayOneShot(healthPSFX);
        transform.position = new Vector3(0f, -90f, 0f);  // can't destroy object before visuals are done
        StartCoroutine(PickupVisual());
        FindObjectOfType<PlayerHealth>().IncreaseHitPoints(hitPointsAmount);
    }

    IEnumerator PickupVisual()
    {
        healthPickupVisual.enabled = true;
        yield return new WaitForSeconds(pickupVisualTime);
        healthPickupVisual.enabled = false;
        Destroy(gameObject);
    }
}
