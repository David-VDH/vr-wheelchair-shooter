using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{
    public float lifeDuration = 1f;

    private void Start()
    {
        DestroyBulletAfterLifetime();
    }

    private void DestroyBulletAfterLifetime()
    {
        Destroy(gameObject, lifeDuration);
    }
}
