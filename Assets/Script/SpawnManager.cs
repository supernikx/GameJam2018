using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<Obstacle> obstacle = new List<Obstacle>();
	public float timeBetweenNewObstacleSpawn,timeBetweenFit;

    List<Obstacle> obstacleCanSpawn = new List<Obstacle>();
    Vector3 spawnPoint;
    float spawnTimer,addTimer,fitSpawnDelay, cantRandomXSPawnPoint;
    int addCounter;
    bool canSpawnFit;
	GameManager gm;

    void Awake()
    {
		gm = FindObjectOfType<GameManager> ();
        addCounter = 0;
        addTimer = 0f;
        canSpawnFit = true;
        fitSpawnDelay = 0f;
        cantRandomXSPawnPoint = -100;
    }

	void Update () {
        if (addCounter!=-1){
            if (addTimer >= obstacle[addCounter].addDelay)
            {
                AddNewObstacle();
            }
        }

        if (fitSpawnDelay > timeBetweenFit)
        {
            canSpawnFit = true;
            cantRandomXSPawnPoint = -100;
        }

        if (spawnTimer > timeBetweenNewObstacleSpawn)
        {
            SpawnObstacle();
        }
	}

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacleCanSpawn.Count);
        if (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fit || obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fixedSpawn )
        {
            if (canSpawnFit)
            {
                canSpawnFit = false;
                fitSpawnDelay = 0;
            }
            else
            {
                do
                {
                    randomIndex = Random.Range(0, obstacleCanSpawn.Count);
                } while (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fit || obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fixedSpawn);
            }
        }
		float screenWidth = gm.background.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 - obstacleCanSpawn[randomIndex].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        if (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fit)
        {
            screenWidth = gm.background.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 - 1f;
        }
        else if (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fixedSpawn)
        {
            screenWidth = 0f;
        }
		float randomXSPawnPoint = Random.Range(-screenWidth, screenWidth);
        
        if (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fit)
        {
            cantRandomXSPawnPoint = randomXSPawnPoint;
        }
        else if (cantRandomXSPawnPoint!=-100f && randomXSPawnPoint>cantRandomXSPawnPoint-1f && randomXSPawnPoint < cantRandomXSPawnPoint + 1)
        {
            if (randomIndex < 0)
            {
                randomXSPawnPoint -= 1f;
            }
            else
            {
                randomXSPawnPoint += 1f;
            }
        }
        spawnPoint = new Vector3(randomXSPawnPoint, Camera.main.orthographicSize, 0f);
        GameObject instantiatedObstacle = Instantiate(obstacleCanSpawn[randomIndex].gameObject, spawnPoint, Quaternion.identity);
        spawnTimer = 0;
		Destroy(instantiatedObstacle, instantiatedObstacle.gameObject.GetComponent<Obstacle>().lifeTime);
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
        fitSpawnDelay += Time.fixedDeltaTime;
    }
}
