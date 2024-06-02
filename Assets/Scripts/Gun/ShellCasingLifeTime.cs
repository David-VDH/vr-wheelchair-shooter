using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCasingLifeTime : MonoBehaviour
{
    public float lifeDuration = 5f;

    private void Start()
    {
        DestroyCasingAfterLifetime();
    }

    private void DestroyCasingAfterLifetime()
    {
        Destroy(gameObject, lifeDuration);
    }
}
