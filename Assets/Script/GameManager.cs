using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	float gUnitCycle;
	int lineNumber = 16;

	public Transform playerTransform;
	public GameObject backgroundPause, omegaBackgroundPause;
	public GameObject[] backgrounds, omegaBackgrounds;//, omegaBackgrounds;
	public int[] phases;
	public GameObject pauseMenu;
	public GameObject gameOverScreen;

	public float[] gridPositions;
	float backgroundSpawnOffset = 0.3f, omegaBackgroundSpawnOffset = .3f;
	float pauseTime = 5f;
	public float timePassed = 0f;
	GameObject lastInstantiatedBackground;
	GameObject lastInstantiatedOmegaBackground;
	GameObject backgroundToInstantiate, omegaBackgroundToInstantiate;
	[HideInInspector]
	public int timeBasedIndex = 0;
	bool isGameOver, isFirstSpawnAfterPause = false;
	float backgroundWidth, gUnit;

	void Awake(){
		backgroundWidth = backgroundPause.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
		gUnit = backgroundWidth/lineNumber;
		gUnitCycle = 0;
		gridPositions = new float[lineNumber];
		lastInstantiatedBackground = Instantiate (backgrounds[timeBasedIndex], Vector3.zero, Quaternion.identity);
		lastInstantiatedOmegaBackground = Instantiate (omegaBackgrounds[timeBasedIndex], Vector3.zero, Quaternion.identity);
		for(int i=0;i<lineNumber;i++){
			gridPositions[i] = -backgroundWidth / 2 + gUnitCycle;
			gUnitCycle += gUnit;
		}
	}

//	void OnDrawGizmos(){
//		backgroundWidth = background.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
//		gUnit = backgroundWidth/16;
//		gUnitCycle = 0;
//		Gizmos.color = Color.red;
//		for(int i=0;i<=16;i++){
//			Vector3 start = new Vector3(-backgroundWidth/2 + gUnitCycle, Camera.main.orthographicSize, 0f);
//			Vector3 end = new Vector3(-backgroundWidth/2 + gUnitCycle, -Camera.main.orthographicSize, 0f);;
//			Gizmos.DrawLine (start, end);
//			gUnitCycle += gUnit;
//		}
//	}

	void Update(){
		timePassed += Time.deltaTime;
		//controllo tempo per fasi
		//se supero fase ma non pausa
		if (timePassed >= phases [timeBasedIndex] && timeBasedIndex + 1 < phases.Length && timePassed <= phases [timeBasedIndex] + pauseTime) {
			backgroundToInstantiate = backgroundPause;
			omegaBackgroundToInstantiate = omegaBackgroundPause;
			backgroundSpawnOffset = lastInstantiatedBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
		} else if (timePassed > phases [timeBasedIndex] + pauseTime && timeBasedIndex + 1 < phases.Length) {
			timeBasedIndex++;
			backgroundToInstantiate = backgrounds [timeBasedIndex];
			omegaBackgroundToInstantiate = omegaBackgrounds [timeBasedIndex];
			backgroundSpawnOffset = .3f;
			isFirstSpawnAfterPause = true;
		} else{
			backgroundToInstantiate = backgrounds [timeBasedIndex];
			omegaBackgroundToInstantiate = omegaBackgrounds [timeBasedIndex];
		}

		//cabio index ogni cambio fase
		if (lastInstantiatedBackground.transform.position.y < -Camera.main.orthographicSize) {
			if (isFirstSpawnAfterPause) {
				backgroundSpawnOffset = lastInstantiatedBackground.GetComponent<SpriteRenderer> ().sprite.bounds.size.y;
				isFirstSpawnAfterPause = false;
			}
			GameObject instantiatedBackground = Instantiate (backgroundToInstantiate, new Vector3(0f, lastInstantiatedBackground.transform.position.y + lastInstantiatedBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (lastInstantiatedBackground, 8f);
			lastInstantiatedBackground = instantiatedBackground;
			backgroundSpawnOffset = .3f;
		}
		if (lastInstantiatedOmegaBackground.transform.position.y <= 0f) {
			GameObject instantiatedOmegaBackground = Instantiate (omegaBackgroundToInstantiate, new Vector3(0f, lastInstantiatedOmegaBackground.transform.position.y + lastInstantiatedOmegaBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - omegaBackgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (lastInstantiatedOmegaBackground, 6f);
			lastInstantiatedOmegaBackground = instantiatedOmegaBackground;
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			ControlMenu ();
	}

	public void ControlMenu(){
		if (pauseMenu.activeSelf) {
			pauseMenu.SetActive (false);
			Time.timeScale = 1f;
		}
		else if (!pauseMenu.activeSelf) {
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
		}
	}

	public void GameOver(){
		if (!isGameOver) {
			gameOverScreen.SetActive (true);
			isGameOver = true;
		}
	}
}
