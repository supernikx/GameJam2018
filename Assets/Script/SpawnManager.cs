using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<Obstacle> obstacle = new List<Obstacle>();
    private List<Obstacle> obstacleCanSpawn;
    public float timeBetweenNewObstacle,timePased;
    Vector3 spawnPoint;
    private float timer;
    private void Awake()
    {
        obstacleCanSpawn = obstacle;

    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timePased += Time.time;
        if (timer > timeBetweenNewObstacle)
        {
            SpawnObstacle();
        }
	}

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacleCanSpawn.Count);
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize - obstacleCanSpawn[randomIndex].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float randomXSPawnPoint = Random.Range(-screenWidth, screenWidth);
        spawnPoint = new Vector3(randomXSPawnPoint, Camera.main.orthographicSize, 0f);
        GameObject instantiatedObstacle = Instantiate(obstacleCanSpawn[randomIndex].gameObject, spawnPoint, Quaternion.identity);
        timer = 0;
        Destroy(instantiatedObstacle, 3f);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }
}
