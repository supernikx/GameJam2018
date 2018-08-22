using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleButton : Obstacle {

	Rigidbody2D rb2d;
	GameManager gm;
	AudioSource death;
    public float xSpawn;
	public GameObject[] buttonsArray;
	public GameObject[] fitObstaclesToSpawn;

	[HideInInspector]
	public int hp;
	float screenWidth;

	void Awake(){
		gm = FindObjectOfType<GameManager> ();
		rb2d = GetComponent<Rigidbody2D>();
		death = GameObject.Find ("ObstacleDeathAudio").GetComponent<AudioSource>();
		rb2d.velocity = new Vector3(0, -this.fallingSpeed, 0);
		hp = buttonsArray.Length;
		screenWidth = gm.backgroundPause.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2- 1f;
	}

	void Update(){
        
		if (hp <= 0)
			Die ();
	}

	void Die(){
		int typeOfFitObstacle = Random.Range(0, fitObstaclesToSpawn.Length);
        xSpawn=gm.GetComponent<SpawnManager>().GetCantXSPawn();
        Vector3 spawnPoint = new Vector3 (xSpawn, transform.position.y, 0f);
		GameObject instantiadetFitObstacle = Instantiate (fitObstaclesToSpawn[typeOfFitObstacle], spawnPoint, Quaternion.identity);
		Destroy (instantiadetFitObstacle, instantiadetFitObstacle.GetComponent<Obstacle>().lifeTime);
		death.Play ();
		Destroy (gameObject);
	}
}
