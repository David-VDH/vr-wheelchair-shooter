using System.Collections;
using UnityEngine;

/// <summary>
/// Modifies a transform's position and rotation to maintain a constant offset with a target transform.
/// Useful for syncing the position/rotation of two objects which are siblings within the hierarchy.
/// </summary>
public class VRWC_FollowTransform : MonoBehaviour
{
    [Tooltip("Transform of the rigidbody to follow.")]

    [SerializeField] private Transform frameTransform;
    [SerializeField] private new Transform camera;
    Vector3 offset;
    Vector3 cameraOffset;
    public bool resetOnStart;

    void Start()
    {
        offset = transform.localPosition - frameTransform.localPosition;
        //if(resetOnStart)
        //StartCoroutine(DelayResetCamera());
    }

     

    void Update()
    {
        Vector3 rotatedOffset = frameTransform.localRotation * (offset - cameraOffset);
        transform.localPosition = frameTransform.localPosition + rotatedOffset;

        transform.rotation = frameTransform.rotation;
    }

    IEnumerator DelayResetCamera()
    {
        yield return new WaitForSeconds(1);
        cameraOffset = camera.localPosition;
        cameraOffset.y = 0;

    }

    [ContextMenu("Reset Camera Offset")]
    public void ResetCameraOffset()
    {
        cameraOffset = camera.localPosition;
        cameraOffset.y = 0;
    }

    //private void OnValidate()
    //{
    //    if (camera == null)
    //        camera = GetComponentInChildren<Camera>().transform;
    //}
}
