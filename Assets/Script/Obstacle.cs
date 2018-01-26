using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float follingSpeed;
    Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0,-follingSpeed,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
