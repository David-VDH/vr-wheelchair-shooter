using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed = 13f;

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

        if(bulletRigidBody != null)
        {
            bulletRigidBody.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
