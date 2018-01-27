using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDrag : Obstacle {

    public float movementspeed;
    private Vector3 offset;
    Rigidbody2D rb2d;
    public bool right;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, -fallingSpeed, 0);
    }

    private void OnMouseOver()
    { 
        if (right)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
            {
                transform.position += new Vector3(movementspeed, 0f, 0f);
            }
        }
        if (!right) { 
            if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
            {
                transform.position += new Vector3(-movementspeed, 0f, 0f);
            }
        }
    }
}
