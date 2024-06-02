using UnityEngine;

public class CasingSpawner : MonoBehaviour
{
    [SerializeField] private Transform casingSpawnPoint;
    [SerializeField] private GameObject casingPrefab;

    [SerializeField] private float casingSpeed = 0.5f;

    public void DropCasing()
    {
        GameObject casing = Instantiate(casingPrefab, casingSpawnPoint.position, casingSpawnPoint.rotation);
        Rigidbody casingRigidBody = casing.GetComponent<Rigidbody>();

        if (casingRigidBody != null)
        {
            casingRigidBody.velocity = (casingSpawnPoint.right) * -1 * casingSpeed;
        }
    }
}
