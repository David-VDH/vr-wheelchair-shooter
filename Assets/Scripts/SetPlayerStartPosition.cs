using UnityEngine;

public class SetPlayerStartPosition : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    private void Start()
    {
        SetStartPosition();
    }

    void SetStartPosition()
    {
        if (spawnPoint != null)
        {
            // Set the player position and rotation to the spawn point's position and rotation
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogError("Spawn point not set in the inspector");
        }
    }
}
