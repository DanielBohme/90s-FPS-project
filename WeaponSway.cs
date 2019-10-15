using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float swayAmount = 0.055f;
    [SerializeField] float maxSwayAmount = 0.09f;

    float smooth = 3;
    float _smooth;

    Vector3 def;
    Vector2 defAth;
    Vector3 euler;

    GameObject ath;

    void Start()
    {
        def = transform.localPosition;
        euler = transform.localEulerAngles;
    }
    
    void Update()
    {
        _smooth = smooth;

        float factorX = -Input.GetAxis("Mouse X") * swayAmount;
        float factorY = -Input.GetAxis("Mouse Y") * swayAmount;

        if (factorX > maxSwayAmount)
        {
            factorX = maxSwayAmount;
        }

        if (factorX < -maxSwayAmount)
        {
            factorX = -maxSwayAmount;
        }

        if (factorY > maxSwayAmount)
        {
            factorY = maxSwayAmount;
        }

        if (factorY < -maxSwayAmount)
        {
            factorY = -maxSwayAmount;
        }

        Vector3 final = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * _smooth);
    }
}
