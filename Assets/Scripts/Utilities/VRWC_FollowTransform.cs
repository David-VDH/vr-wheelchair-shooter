using System.Collections;
using UnityEngine;

/// <summary>
/// Modifies a transform's position and rotation to maintain a constant offset with a target transform.
/// Useful for syncing the position/rotation of two objects which are siblings within the hierarchy.
/// </summary>
public class VRWC_FollowTransform : MonoBehaviour
{
    [Tooltip("Transform of the rigidbody to follow.")]

    public Transform target;
    //public new Transform camera;
    Vector3 offset;
    Vector3 cameraOffset;

    void Start()
    {
        offset = transform.localPosition - target.localPosition;
    }

    void Update()
    {
        Vector3 rotatedOffset = target.localRotation * (offset - cameraOffset);
        transform.localPosition = target.localPosition + rotatedOffset;

        transform.rotation = target.rotation;
    }
}
