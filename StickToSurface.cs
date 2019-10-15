using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class StickToSurface : MonoBehaviour
{
    private Collider myCollider;
    private Rigidbody rb;
    private Transform stickParent;

    private bool hasStuck = false;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasStuck)
        {
            if (stickParent != null)
            {
                transform.position = stickParent.transform.position;
                transform.rotation = stickParent.transform.rotation;
            }

            else
            {
                myCollider.isTrigger = false;
                rb.isKinematic = false;
                hasStuck = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasStuck)
        {
            return;
        }

        transform.position = collision.contacts[0].point;
        myCollider.isTrigger = true;

        GameObject newParent = new GameObject("StickTransform");
        newParent.transform.position = transform.position;
        newParent.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        newParent.transform.parent = collision.transform;
        stickParent = newParent.transform;

        rb.isKinematic = true;

        hasStuck = true;
    }
}
