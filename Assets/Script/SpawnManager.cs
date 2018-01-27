using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<Obstacle> obstacle = new List<Obstacle>();
    private List<Obstacle> obstacleCanSpawn = new List<Obstacle>();
    public float timeBetweenNewObstacleSpawn,timePased;
    private Vector3 spawnPoint;
    private float spawnTimer,addTimer;
    private int addCounter;
    private void Awake()
    {
        addCounter = 0;
        addTimer = 0f;
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timePased += Time.time;
        if (addCounter!=-1){
            if (addTimer >= obstacle[addCounter].addDelay)
            {
                AddNewObstacle();
            }
        }

        if (spawnTimer > timeBetweenNewObstacleSpawn)
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
        spawnTimer = 0;
        Destroy(instantiatedObstacle, 3f);
    }

    private void AddNewObstacle()
    {
        obstacleCanSpawn.Add(obstacle[addCounter]);
        addTimer = 0;
        if (addCounter + 1 < obstacle.Count)
        {
            addCounter++;
        }
        else
        {
            addCounter = -1;
        }
    }

    private void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        addTimer += Time.fixedDeltaTime;
    }
}
