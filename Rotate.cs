using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 rotateVector = new Vector3(1f, 1f, 1f);
    [SerializeField] float idleSpeed = 0.4f;
    [SerializeField] float fireSpeed = 0.4f;
    
    void Update()
    {
        if (Input.GetMouseButton(0) & FindObjectOfType<Ammo>().GetCurrentAmmo(ammoType: 0) > 0)
        {
            transform.Rotate(rotateVector * fireSpeed * Time.deltaTime);
        }

        else
        {
            transform.Rotate(rotateVector * idleSpeed * Time.deltaTime);
        }
    }
}
