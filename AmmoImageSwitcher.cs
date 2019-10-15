using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoImageSwitcher : MonoBehaviour
{
    [SerializeField] public int currentAmmoImage = 0;

    void Start()
    {
        SetAmmoImageActive();
    }

    void Update()
    {
        int previousAmmoImage = currentAmmoImage;

        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousAmmoImage != currentAmmoImage)
        {
            SetAmmoImageActive();
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentAmmoImage >= transform.childCount - 1)
            {
                currentAmmoImage = 0;
            }

            else
            {
                currentAmmoImage++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentAmmoImage <= 0)
            {
                currentAmmoImage = transform.childCount - 1;
            }

            else
            {
                currentAmmoImage--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentAmmoImage = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentAmmoImage = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentAmmoImage = 2;
        }
    }

    private void SetAmmoImageActive()
    {
        int ammoImageIndex = 0;

        foreach (Transform ammoImage in transform)
        {
            if (ammoImageIndex == currentAmmoImage)
            {
                ammoImage.gameObject.SetActive(true);
            }

            else
            {
                ammoImage.gameObject.SetActive(false);
            }

            ammoImageIndex++;
        }
    }
}
