using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{
    public float lifeDuration = 2f;

    private void Start()
    {
        DestroyBulletAfterLifetime();
    }

    private void DestroyBulletAfterLifetime()
    {
        Destroy(gameObject, lifeDuration);
    }
}
