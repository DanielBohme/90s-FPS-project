using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] RawImage ammoPickupVisual;
    [SerializeField] AudioClip ammoPSFX;

    public float pickupVisualTime = 0.2f;

    AudioSource audioSource;

    private void Start()
    {
        ammoPickupVisual.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(ammoPSFX);
            transform.position = new Vector3(0f, -90f, 0f);
            StartCoroutine(PickupVisual());
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
        }
    }

    IEnumerator PickupVisual()
    {
        ammoPickupVisual.enabled = true;
        yield return new WaitForSeconds(pickupVisualTime);
        ammoPickupVisual.enabled = false;
        Destroy(gameObject);
    }
}
