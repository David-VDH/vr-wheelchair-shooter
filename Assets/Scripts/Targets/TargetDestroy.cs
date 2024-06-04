using UnityEngine;

public class TargetDestroy : MonoBehaviour
{
    private bool isTargetDestroyed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.collider.CompareTag("Bullet"))
        {
            DestroyTarget();
        }
    }

    void DestroyTarget()
    {
        if(!isTargetDestroyed)
        {
            Destroy(gameObject);
            Score.instance.TargetDestroyed();
            isTargetDestroyed = true;
        }
    }
}
