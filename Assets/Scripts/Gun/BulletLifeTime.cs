using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{
    [SerializeField] private float lifeDuration = 2.5f;

    private void Start()
    {
        DestroyBulletAfterLifetime();
    }

    private void DestroyBulletAfterLifetime()
    {
        Destroy(gameObject, lifeDuration);
    }
}
