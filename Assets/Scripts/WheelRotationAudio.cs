using UnityEngine;

public class WheelRotationAudio : MonoBehaviour
{
    public Rigidbody targetRigidbody;
    public AudioClip rotationClip;
    public float rotationThreshold = 0.1f; // Minimum angular velocity to be considered rotating

    private AudioManager audioManager;
    private bool isPlaying = false;

    private float lastXRotation;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Make sure an AudioManager script is present in the scene.");
        }

        lastXRotation = targetRigidbody.rotation.eulerAngles.x;
    }

    void Update()
    {
        if (IsXRotating(targetRigidbody))
        {
            if (!isPlaying)
            {
                audioManager.PlayAudioClip(rotationClip);
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                audioManager.GetComponent<AudioSource>().Stop();
                isPlaying = false;
            }
        }
    }

    private bool IsXRotating(Rigidbody rb)
    {
        float currentXRotation = rb.rotation.eulerAngles.x;
        float rotationDifference = Mathf.Abs(Mathf.DeltaAngle(lastXRotation, currentXRotation));

        return rotationDifference > rotationThreshold;
    }
}
