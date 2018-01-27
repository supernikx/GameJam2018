using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform playerTransform;
	public GameObject background;
	public GameObject pauseMenu;

	float backgroundSpawnOffset = 0.3f;
	GameObject lastInstantiatedBackground;

	void Start(){
		lastInstantiatedBackground = Instantiate (background, Vector3.zero, Quaternion.identity);
	}

	void Update(){
		if (lastInstantiatedBackground.transform.position.y < -Camera.main.orthographicSize) {
			GameObject instantiatedBackground = Instantiate (background, new Vector3(0f, lastInstantiatedBackground.transform.position.y + lastInstantiatedBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (lastInstantiatedBackground, 8f);
			lastInstantiatedBackground = instantiatedBackground;
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			ControlMenu ();
	}

	public void ControlMenu(){
		if (pauseMenu.activeSelf)
			pauseMenu.SetActive (false);
		else if (!pauseMenu.activeSelf)
			pauseMenu.SetActive (true);
	}
}
