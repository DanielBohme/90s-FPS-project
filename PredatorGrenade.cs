using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorGrenade : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] float explosionForce = 1000;
    [SerializeField] AudioClip explosionSFX;

    AudioSource audioSource;
    GameObject mainCamera;

    public float radius = 20f;
    private float remainingTime;
    [Range(10f, 120f)] public float damage = 120f;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("Player");
        mainCamera.GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        remainingTime = lifeTime;
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            Explode();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, .45f);
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    private void Explode()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position);   // audio low on grenade position. use main camera position
        var instance = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(instance.gameObject, 0.2f);
        
        Collider[] objectsInRange = Physics.OverlapSphere(this.transform.position, radius);

        foreach (Collider col in objectsInRange)
        {
            if (col.GetComponent<EnemyHealth>())
            {
                float proximity = (this.transform.position - col.transform.position).magnitude;
                float effect = 1 - (proximity / radius);
                effect = Mathf.Clamp(effect, 0f, 1f);

                // Apply damage
                if (col.gameObject.GetComponent<EnemyHealth>())
                {
                    col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage * effect);
                    // Debug.Log(col.name + " health: " + col.gameObject.GetComponent<EnemyHealth>().hitPoints);
                }
            }

            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
        
        Destroy(gameObject);
    }
}