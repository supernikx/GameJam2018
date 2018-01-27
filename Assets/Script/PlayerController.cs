using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;
	GameManager gm;

	Transform flatPlayer, rotatedPlayer;
	SpriteRenderer mySprite;
	float screenWidth;
	Vector3 moveVelocity;
    [HideInInspector]
	public bool isFlat = true;

	void Awake(){
		gm = FindObjectOfType<GameManager> ();
		flatPlayer = transform.GetChild (0);
		rotatedPlayer = transform.GetChild (1);
		mySprite = flatPlayer.GetComponent<SpriteRenderer>();
		screenWidth = gm.background.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 - mySprite.sprite.bounds.size.x/2;
	}
		
	void Update () {
		Vector3 inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, 0f).normalized;
		moveVelocity = inputDirection * moveSpeed * Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			isFlat = !isFlat;
			ChangePlayerForm ();
		}
	}

	void FixedUpdate(){
		transform.position += moveVelocity;
		if (transform.position.x < -screenWidth)
			transform.position = new Vector3 (-screenWidth,transform.position.y,0);
		if(transform.position.x > screenWidth)
			transform.position = new Vector3 (screenWidth,transform.position.y,0);
	}

	public void Die(){
		Destroy (gameObject);
	}

	public void ChangePlayerForm(){
		flatPlayer.gameObject.SetActive (isFlat);
		rotatedPlayer.gameObject.SetActive (!isFlat);
		if(rotatedPlayer.gameObject.activeSelf)
			mySprite = rotatedPlayer.GetComponent<SpriteRenderer>();
		else
			mySprite = flatPlayer.GetComponent<SpriteRenderer>();
		screenWidth = Camera.main.aspect * Camera.main.orthographicSize - mySprite.sprite.bounds.size.x/2;
	}
}
