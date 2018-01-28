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
		float screenWidth = gm.backgroundPause.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 - obstacleCanSpawn[randomIndex].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
		float randomXSPawnPoint = Random.Range(-screenWidth, screenWidth);
        if (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fit || obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fixedSpawn)
        {
			int randomGridPosition = Random.Range (1, gm.gridPositions.Length - 1);
			randomXSPawnPoint = gm.gridPositions [randomGridPosition];
            cantRandomXSPawnPoint = randomXSPawnPoint;
            if (obstacleCanSpawn[randomIndex].type == Obstacle.ObstacleType.fixedSpawn)
            {
                randomXSPawnPoint = 0f;
            }
        }else if (cantRandomXSPawnPoint != -100f && randomXSPawnPoint > cantRandomXSPawnPoint - 2f && randomXSPawnPoint < cantRandomXSPawnPoint + 2f)
            {
             if (randomXSPawnPoint > -2f && randomXSPawnPoint < 2f)
            {
                if (randomXSPawnPoint > -2f && randomXSPawnPoint < 0f)
                    randomXSPawnPoint -= 3f;
                else if (randomXSPawnPoint > 0f && randomXSPawnPoint < 2f)
                    randomXSPawnPoint += 3f;
            }
            else
                {
                    randomXSPawnPoint *= -1;
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

    public float GetCantXSPawn()
    {
        return cantRandomXSPawnPoint;
    }
}
