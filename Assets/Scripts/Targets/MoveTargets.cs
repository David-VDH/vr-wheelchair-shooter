using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float changeDirectionInterval;

    private Vector2 movementDirection;
    private Rigidbody targetRB;

    private float minY = 0.5f;
    private float maxY = 4f;

    void Start()
    {
        changeDirectionInterval = Random.Range(2f, 4f);
        targetRB = GetComponent<Rigidbody>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    void FixedUpdate()
    {
        targetRB.velocity = movementDirection * moveSpeed;

        Vector3 clampedPosition = targetRB.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        targetRB.position = clampedPosition;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionInterval);
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        // Get a random direction
        float angle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void OnCollisionEnter(Collision collision)
    {
        // Change direction upon collision
        ChangeDirection();
    }
}

