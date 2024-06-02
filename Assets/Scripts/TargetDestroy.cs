using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.collider.CompareTag("Bullet"))
        {
            DestroyTarget();
        }
    }

    void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
