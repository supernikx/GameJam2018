using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleButton : Obstacle {

	Rigidbody2D rb2d;
	GameManager gm;

	public GameObject[] buttonsArray;
	public GameObject[] fitObstaclesToSpawn;

	[HideInInspector]
	public int hp;
	float screenWidth;

	void Awake(){
		gm = FindObjectOfType<GameManager> ();
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = new Vector3(0, -this.fallingSpeed, 0);
		hp = buttonsArray.Length;
		screenWidth = gm.background.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2- 1f;
	}

	void Update(){
        
		if (hp <= 0)
			Die ();
	}

	void Die(){
		int randomIndex = Random.Range(0, fitObstaclesToSpawn.Length);
		float randomXSPawnPoint = Random.Range(-screenWidth, screenWidth);
		Vector3 spawnPoint = new Vector3 (randomXSPawnPoint, transform.position.y, 0f);
		GameObject instantiadetFitObstacle = Instantiate (fitObstaclesToSpawn[randomIndex], spawnPoint, Quaternion.identity);
		Destroy (instantiadetFitObstacle, instantiadetFitObstacle.GetComponent<Obstacle>().lifeTime);
		Destroy (gameObject);
	}
}
