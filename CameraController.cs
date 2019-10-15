using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float lookSmoothing;
    [MinMaxSlider(-90f, 90f)] public Vector2 lookAngleMinMax;

    private Transform playerTransform;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingDirection;

    float pitch;

    private void Awake()
    {
        playerTransform = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        Vector2 cameraRotationInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        cameraRotationInput = Vector2.Scale(cameraRotationInput, new Vector2(lookSensitivity * lookSmoothing, lookSensitivity * lookSmoothing));
        smoothedVelocity = Vector2.Lerp(smoothedVelocity, cameraRotationInput, 1 / lookSmoothing);

        currentLookingDirection.y = Mathf.Clamp(currentLookingDirection.y, lookAngleMinMax.x, lookAngleMinMax.y);

        currentLookingDirection += smoothedVelocity;

        transform.localRotation = Quaternion.AngleAxis(-currentLookingDirection.y, Vector3.right);
        playerTransform.localRotation = Quaternion.AngleAxis(currentLookingDirection.x, playerTransform.up);
    }
}
