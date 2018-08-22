using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	float gUnitCycle;
	int lineNumber = 16;
	public float tempoDis;
	public AudioSource music;

	public Transform playerTransform;
	public GameObject backgroundPause, omegaBackgroundPause, bridgePrefab;
	public GameObject[] backgrounds, omegaBackgrounds;//, omegaBackgrounds;
	public int[] phases;
	public GameObject pauseMenu;
	public GameObject gameOverScreen;
	public GameObject endScreen;

	public float[] gridPositions;
	float backgroundSpawnOffset = 0.3f, omegaBackgroundSpawnOffset = .3f;
	float pauseTime = 5f;
	public float timePassed = 0f;
	SpawnManager spawnManager;
	GameObject lastInstantiatedBackground;
	GameObject lastInstantiatedOmegaBackground;
	GameObject lastInstantiatedBridge;
	GameObject backgroundToInstantiate, omegaBackgroundToInstantiate;
	[HideInInspector]
	public int timeBasedIndex = 0;
	bool isGameOver, isGameEnded, isFirstSpawnAfterPause = false, isFirstTimeInIf = true;
	float backgroundWidth, gUnit;

	void Awake(){
		music.Play ();
		spawnManager = GetComponent<SpawnManager> ();
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
		//inizio pausa

		if (timePassed >= phases [timeBasedIndex] && timeBasedIndex + 1 < phases.Length && timePassed <= phases [timeBasedIndex] + pauseTime) {
			backgroundToInstantiate = backgroundPause;
			omegaBackgroundToInstantiate = omegaBackgroundPause;
			if (isFirstTimeInIf) {
				isFirstSpawnAfterPause = true;
				isFirstTimeInIf = false;
			}
		} else if (timePassed > phases [timeBasedIndex] + pauseTime && timeBasedIndex + 1 < phases.Length) {
			timeBasedIndex++;
			backgroundToInstantiate = backgrounds [timeBasedIndex];
			omegaBackgroundToInstantiate = omegaBackgrounds [timeBasedIndex];
			isFirstSpawnAfterPause = true;
			isFirstTimeInIf = true;
		} else{
			backgroundToInstantiate = backgrounds [timeBasedIndex];
			omegaBackgroundToInstantiate = omegaBackgrounds [timeBasedIndex];
		}

		//cabio index ogni cambio fase
		if (lastInstantiatedBackground.transform.position.y < -Camera.main.orthographicSize) {
			if (isFirstSpawnAfterPause) {
				GameObject instantiatedBridge = Instantiate (bridgePrefab, new Vector3 (0f, lastInstantiatedBackground.transform.position.y + lastInstantiatedBackground.GetComponent<SpriteRenderer> ().sprite.bounds.size.y/2, 0f), Quaternion.identity);
				Destroy (instantiatedBridge, 8f);
				lastInstantiatedBridge = instantiatedBridge;
				isFirstSpawnAfterPause = false;
				Invoke ("SwitchSpawnOnOff", 1.5f);
			}
			GameObject instantiatedBackground = Instantiate (backgroundToInstantiate, new Vector3(0f, lastInstantiatedBackground.transform.position.y + lastInstantiatedBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			GameObject instantiatedOmegaBackground = Instantiate (omegaBackgroundToInstantiate, new Vector3(0f, lastInstantiatedOmegaBackground.transform.position.y + lastInstantiatedOmegaBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (lastInstantiatedBackground, 8f);
			Destroy (lastInstantiatedOmegaBackground, 6f);
			lastInstantiatedBackground = instantiatedBackground;
			lastInstantiatedOmegaBackground = instantiatedOmegaBackground;
		}

		//check omega spawn
//		if (lastInstantiatedOmegaBackground.transform.position.y <= 0f) {
//			GameObject instantiatedOmegaBackground = Instantiate (omegaBackgroundToInstantiate, new Vector3(0f, lastInstantiatedOmegaBackground.transform.position.y + lastInstantiatedOmegaBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - omegaBackgroundSpawnOffset, 0f), Quaternion.identity);
//			Destroy (lastInstantiatedOmegaBackground, 6f);
//			lastInstantiatedOmegaBackground = instantiatedOmegaBackground;
//		}

		//pause menu
		if (Input.GetKeyDown (KeyCode.Escape))
			ControlMenu ();

		if (timePassed >= phases [phases.Length - 1])
			ShowEndScreen ();
	}

	public void ControlMenu(){
		if (pauseMenu.activeSelf) {
			pauseMenu.SetActive (false);
			Time.timeScale = 1f;
			music.UnPause ();
		}
		else if (!pauseMenu.activeSelf) {
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
			music.Pause ();
		}
	}

	public void GameOver(){
		if (!isGameOver && !isGameEnded) {
			gameOverScreen.SetActive (true);
			isGameOver = true;
		}
	}

	void SwitchSpawnOnOff(){
		spawnManager.enabled = !spawnManager.enabled;
	}

	void ShowEndScreen(){
		if (!isGameEnded && !isGameOver) {
			endScreen.SetActive (true);
			isGameEnded = true;
			playerTransform.GetComponent<PlayerController> ().enabled = false;
			try{
			playerTransform.GetComponentInChildren<CircleCollider2D> ().enabled = false;
			}catch(Exception e){
			playerTransform.GetComponentInChildren<BoxCollider2D> ().enabled = false;
			}
		}
	}
}
