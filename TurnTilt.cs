using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TurnTilt : MonoBehaviour
{
    [SerializeField] float tiltFactor = -5f;
    float xThrow;

    public void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    public void ProcessRotation()
    {
        float roll = xThrow * tiltFactor;                
        transform.localRotation = Quaternion.Euler(roll, 0, 0); 
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    }
}
