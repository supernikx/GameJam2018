using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriangle : Obstacle {

    public float rotatingSpeed;
    Rigidbody2D rb2d;
    private int rotationdirection;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, -fallingSpeed, 0);
        rotationdirection = Random.Range(0,2);
    }
	
	// Update is called once per frame
	void Update () {
        if (rotationdirection == 0)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotatingSpeed);
        }
        else if (rotationdirection == 1)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * rotatingSpeed);
        }
	}
}
