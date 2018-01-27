using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Obstacle {

    private Vector3 startScale, scaleToReach;
    public float scaleSpeed;
    private bool expand;
    public float rotatingSpeed;
    Rigidbody2D rb2d;
    private int rotationdirection;
    private void Start()
    {
        expand = true;
        startScale = transform.localScale;
        scaleToReach = startScale + new Vector3(0.6f, 0.6f, 0f);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, -fallingSpeed, 0);
        rotationdirection = Random.Range(0, 2);
    }
    // Update is called once per frame
    void Update () {
        if (expand)
        {
            transform.localScale += new Vector3(scaleSpeed*Time.deltaTime, scaleSpeed * Time.deltaTime, 0f);
            if (transform.localScale.x>scaleToReach.x && transform.localScale.y > scaleToReach.y)
            {
                expand = false;
            }
        }
        if (!expand)
        {
            transform.localScale -= new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, 0f);
            if (transform.localScale.x < startScale.x && transform.localScale.y < startScale.y)
            {
                expand = true;
            }
        }

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
