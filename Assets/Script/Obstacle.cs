using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour {
	
    public float fallingSpeed;
    public float addDelay;
	public float lifeTime;
	public ObstacleType type;

    Rigidbody2D rb2d;

	public enum ObstacleType{
		projectile, fixedSpawn, fit
	}
		
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, -fallingSpeed, 0);
    }
}
