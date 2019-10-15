using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadDamage : MonoBehaviour
{
    [SerializeField] Canvas quadDamage;
    [SerializeField] AudioClip quadDSFX;

    public float quadDamageDuration = 8f;
    float timeLeft;
    public Image timerBar;
    
    AudioSource audioSource;

    private void Start()
    {
        quadDamage.enabled = false;
        timeLeft = quadDamageDuration;
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Pickup(other));
            StartCoroutine(Timer(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        audioSource.PlayOneShot(quadDSFX);
        quadDamage.enabled = true;

        FindObjectOfType<Weapon>().IncreaseDamage();
        
        // Can not turn off game object, changing position to under map. See how demanding this is or turn of particle system.
        transform.position = new Vector3(0f, -90f, 0f);

        yield return new WaitForSeconds(quadDamageDuration);

        FindObjectOfType<Weapon>().NormalDamage();
        quadDamage.enabled = false;

        Destroy(gameObject);
    }

    IEnumerator Timer(Collider player)
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            float ratio = timeLeft / quadDamageDuration;
            timerBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
            yield return null;
        }
    }
}
