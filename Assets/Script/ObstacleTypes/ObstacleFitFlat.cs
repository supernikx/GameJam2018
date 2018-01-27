using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFitFlat : Obstacle {
	
	Rigidbody2D rb2d;
    public bool isFlat;
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = new Vector3(0, -fallingSpeed, 0);
	}

	//check player form?
	//or let the colliders decide
}
