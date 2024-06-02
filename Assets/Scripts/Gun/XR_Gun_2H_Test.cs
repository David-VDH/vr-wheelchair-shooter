using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_Gun_2H_Test : XRBaseInteractable
{
    [Serializable]
    public class AxisRotationSettings
    {
        public Transform handle;
        public float minValue = -45.0f;
        public float maxValue = 45.0f;
        public float currentValue = 0.5f;
        public float sensitivity = 100f;
    }

    [SerializeField]
    private AxisRotationSettings verticalSettings = new AxisRotationSettings();
    [SerializeField]
    private AxisRotationSettings horizontalSettings = new AxisRotationSettings();

    private IXRSelectInteractor primaryInteractor;
    private IXRSelectInteractor secondaryInteractor;

    private Vector3 gunBaseLocalPosition;
    [SerializeField] private Transform gunBaseTransform;

    public bool isDualInteractionActive; //Flag to detect dual interaction

    public float recoilStrength = 0.85f; //Recoil strength magnitude
    public Vector2 recoilAngleRange = new Vector2(-1f, 1f); //Random recoil angle range

    public bool canShoot = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(StartGrab);
        selectExited.AddListener(EndGrab);
    }

    protected override void OnDisable()
    {
        selectEntered.RemoveListener(StartGrab);
        selectExited.RemoveListener(EndGrab);
        base.OnDisable();
    }

    void StartGrab(SelectEnterEventArgs args)
    {
        if (primaryInteractor == null)
        {
            primaryInteractor = args.interactorObject;

            // Adjust basePosition to be the offset from the collider's transform
            gunBaseLocalPosition = gunBaseTransform.InverseTransformPoint(primaryInteractor.transform.position);
        }
        else if (secondaryInteractor == null)
        {
            secondaryInteractor = args.interactorObject;
            isDualInteractionActive = true;
        }
    }

    void EndGrab(SelectExitEventArgs args)
    {
        if (args.interactorObject == primaryInteractor)
        {
            if (secondaryInteractor != null)
            {
                // Reassign the primary interactor to the secondary before it is cleared.
                primaryInteractor = secondaryInteractor;
                secondaryInteractor = null;

                // Update the basePosition to the current position of the new primary interactor.
                gunBaseLocalPosition = gunBaseTransform.InverseTransformPoint(primaryInteractor.transform.position);
            }
            else
            {
                primaryInteractor = null;
            }

            isDualInteractionActive = false;
        }
        else if (args.interactorObject == secondaryInteractor)
        {
            secondaryInteractor = null;
            isDualInteractionActive = false;
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected && primaryInteractor != null)
            {
                UpdateRotation();
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }   
        }
    }

    public void ApplyRecoil()
    {
        if (!isDualInteractionActive) // Apply recoil only if one hand is being used
        {
            float recoilAngleX = UnityEngine.Random.Range(recoilAngleRange.x, recoilAngleRange.y);
            float recoilAngleY = UnityEngine.Random.Range(recoilAngleRange.x, recoilAngleRange.y);
            Quaternion recoilRotation = Quaternion.Euler(recoilAngleX, recoilAngleY, 0);

            // Apply the recoil rotation
            verticalSettings.handle.rotation *= recoilRotation;
        }

        // Reassert the Z-axis rotation to be zero
        Vector3 currentEulerAngles = verticalSettings.handle.eulerAngles;
        verticalSettings.handle.eulerAngles = new Vector3(currentEulerAngles.x, currentEulerAngles.y, 0);
    }

    void UpdateRotation()
    {
        Vector3 currentInteractorPosition = primaryInteractor.transform.position;
        Vector3 baseWorldPosition = transform.TransformPoint(gunBaseLocalPosition);
        Vector3 displacement = currentInteractorPosition - baseWorldPosition;

        //transform the displacement to local space of the gunBaseTransform
        Vector3 localDisplacement = gunBaseTransform.InverseTransformVector(displacement);

        ApplyRotation(verticalSettings, localDisplacement.y);
        ApplyRotation(horizontalSettings, localDisplacement.x);
    }

    void ApplyRotation(AxisRotationSettings settings, float displacement)
    {
        float rotation = displacement * settings.sensitivity;  // Convert displacement to rotation based on sensitivity

        float clampedAngle = Mathf.Clamp(rotation, settings.minValue, settings.maxValue);

        settings.currentValue = Mathf.InverseLerp(settings.minValue, settings.maxValue, clampedAngle);

        if (settings.handle != null)
        {
            if (settings == verticalSettings)
            {
                settings.handle.localEulerAngles = new Vector3(clampedAngle, settings.handle.localEulerAngles.y, settings.handle.localEulerAngles.z);
            }
            else if (settings == horizontalSettings)
            {
                settings.handle.localEulerAngles = new Vector3(settings.handle.localEulerAngles.x, -clampedAngle, settings.handle.localEulerAngles.z);
            }
        }
    }
}
