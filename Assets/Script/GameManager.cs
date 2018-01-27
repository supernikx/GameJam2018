using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform playerTransform;
	public GameObject background, omegaBackground;
	public GameObject pauseMenu;
	public GameObject gameOverScreen;

	float backgroundSpawnOffset = 0.3f;
	float timePassed = 0f;
	GameObject lastInstantiatedBackground;
	GameObject lastInstantiatedOmegaBackground;
	bool isGameOver;

	void Start(){
		lastInstantiatedBackground = Instantiate (background, Vector3.zero, Quaternion.identity);
		lastInstantiatedOmegaBackground = Instantiate (omegaBackground, Vector3.zero, Quaternion.identity);
	}

	void Update(){
		timePassed += Time.time;
		if (lastInstantiatedBackground.transform.position.y < -Camera.main.orthographicSize) {
			GameObject instantiatedBackground = Instantiate (background, new Vector3(0f, lastInstantiatedBackground.transform.position.y + lastInstantiatedBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (lastInstantiatedBackground, 8f);
			lastInstantiatedBackground = instantiatedBackground;
		}
		if (lastInstantiatedOmegaBackground.transform.position.y <= 0f) {
			GameObject instantiatedOmegaBackground = Instantiate (omegaBackground, new Vector3(0f, lastInstantiatedOmegaBackground.transform.position.y + lastInstantiatedOmegaBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (lastInstantiatedBackground, 8f);
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
