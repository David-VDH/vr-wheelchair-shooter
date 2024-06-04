using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject gunBarrel;

    [SerializeField] XR_Gun_2H_Test xR_Gun_2H_Test;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] CasingSpawner casingSpawner;

    private float triggerValue = 0f;

    private float accumulatedRotation = 0.0f;
    private float rotationPerBullet = 60.0f;

    private List<InputDevice> devicesWithTrigger;

    private void Awake()
    {
        devicesWithTrigger = new List<InputDevice>();
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);

        foreach (InputDevice device in allDevices)
        {
            InputDevices_deviceConnected(device);
        }

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;

        devicesWithTrigger.Clear();
    }

    //This function adds any devices that have a trigger.
    private void InputDevices_deviceConnected(InputDevice device)
    {
        float discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.trigger, out discardedValue))
        {
            devicesWithTrigger.Add(device); 
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithTrigger.Contains(device))
            devicesWithTrigger.Remove(device);
    }

    void Update()
    {
        CheckForShooting();
    }

    private void CheckForShooting()
    {
        bool hasShotThisFrame = false;

        foreach (var device in devicesWithTrigger)
        {
            if (hasShotThisFrame)
            {
                break;
            }

            if (device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue) && triggerValue > 0.2f && xR_Gun_2H_Test.canShoot == true)
            {
                Shoot(triggerValue);
                hasShotThisFrame = true;
            }
        }
    }

    public void Shoot(float triggerValue)
    {
        float rotationThisFrame = -triggerValue * 25;
        gunBarrel.transform.Rotate(0, 0, rotationThisFrame);
        accumulatedRotation += Mathf.Abs(rotationThisFrame);

        while (accumulatedRotation >= rotationPerBullet)
        {
            bulletSpawner.FireBullet();
            accumulatedRotation -= rotationPerBullet;
            casingSpawner.DropCasing();
        }

        xR_Gun_2H_Test.ApplyRecoil();
        SendHapticFeedback();
    }

    private void SendHapticFeedback()
    {
        foreach (var device in devicesWithTrigger)
        {
            bool isPullingTrigger;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out isPullingTrigger) && isPullingTrigger)
            {
                device.SendHapticImpulse(1, 1f, 0.1f);
            }
        }
    }
}