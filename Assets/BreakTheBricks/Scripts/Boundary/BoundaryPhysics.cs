using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Ball>(out Ball ball))
        {
            ball.direction = Vector2.Reflect(ball.direction,transform.up);
        }
    }
}
