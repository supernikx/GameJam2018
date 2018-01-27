using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform playerTransform;
	public GameObject background;

	float backgroundSpawnOffset = 0.1f;
	GameObject lastInstantiatedBackground;

	void Start(){
		lastInstantiatedBackground = Instantiate (background, Vector3.zero, Quaternion.identity);
	}

	void Update(){
		if (lastInstantiatedBackground.transform.position.y < -Camera.main.orthographicSize) {
			GameObject instantiatedBackground = Instantiate (background, new Vector3(0f, lastInstantiatedBackground.transform.position.y + lastInstantiatedBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y - backgroundSpawnOffset, 0f), Quaternion.identity);
			Destroy (instantiatedBackground, 8f);
			lastInstantiatedBackground = instantiatedBackground;
		}
	}
}
