using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour {
	
    public float fallingSpeed;
    public float addDelay;
	public float lifeTime;
	public ObstacleType type;

	public enum ObstacleType{
		projectile, fixedSpawn, fit
	}
		
    void Start () {
    }
}
