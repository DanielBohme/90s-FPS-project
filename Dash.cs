using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovementController))]
public class Dash : Ability
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashRechargeTime;
    [SerializeField] private int maxDashes = 3;

    [SerializeField] AudioClip dashSFX;

    AudioSource audioSource;

    public RawImage dashImage;
    public RawImage dashImage2;
    public Canvas dash3;
    public Canvas dash2;
    public Canvas dash1;

    private float currentDashRechargeTime = 0f;
    private int remainingDashes = 3;

    private PlayerMovementController playerMovementController;

    private void Start()
    {
        dash3.enabled = true;
        dash2.enabled = false;
        dash1.enabled = false;

        dashImage.enabled = false;
        dashImage2.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Cast());
        }

        if (remainingDashes < maxDashes)
        {
            currentDashRechargeTime += Time.deltaTime;

            if (currentDashRechargeTime >= dashRechargeTime)
            {
                remainingDashes++;
                currentDashRechargeTime = 0f;
            }
        }

        switch (remainingDashes)
        {
            case 3:
                dash3.enabled = true;
                dash2.enabled = true;
                dash1.enabled = true;
                break;
            case 2:
                dash3.enabled = false;
                dash2.enabled = true;
                dash1.enabled = true;
                break;
            case 1:
                dash3.enabled = false;
                dash2.enabled = false;
                dash1.enabled = true;
                break;
            case 0:
                dash3.enabled = false;
                dash2.enabled = false;
                dash1.enabled = false;
                break;
        }
    }

    public override IEnumerator Cast()
    {
        if (remainingDashes <= 0)
        {
            yield break;
        }

        remainingDashes--;

        audioSource.PlayOneShot(dashSFX);

        dashImage.enabled = true;
        dashImage2.enabled = true;

        Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (movementInput.magnitude == 0f)
        {
            movementInput.z = 1f;
        }

        movementInput = transform.TransformDirection(movementInput);

        playerMovementController.AddForce(movementInput, dashForce);

        yield return new WaitForSeconds(dashDuration);

        dashImage.enabled = false;
        dashImage2.enabled = false;

        playerMovementController.ResetImpact();
    }
}
