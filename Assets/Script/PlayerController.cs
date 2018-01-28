using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public float speedWhilePressing = .15f;
	GameManager gm;
	int myGridPosition;

	Transform flatPlayer, rotatedPlayer;
	SpriteRenderer mySprite;
	float screenWidth;
	float movingTimer;
	Vector3 moveVelocity;
    [HideInInspector]
	public bool isFlat = true;

	void Start(){
		gm = FindObjectOfType<GameManager> ();
		flatPlayer = transform.GetChild (0);
		rotatedPlayer = transform.GetChild (1);
		mySprite = flatPlayer.GetComponent<SpriteRenderer>();
		screenWidth = gm.backgrounds[gm.timeBasedIndex].GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 - mySprite.sprite.bounds.size.x/2;
		myGridPosition = gm.gridPositions.Length / 2;
		transform.position = new Vector3 (gm.gridPositions[myGridPosition], transform.position.y, 0f);
	}
		
	void Update () {
		//movement on key down
		if (Input.GetKeyDown (KeyCode.A)) {
			if (myGridPosition - 1 > 0) {
				myGridPosition -= 1;
				transform.position = new Vector3 (gm.gridPositions [myGridPosition], transform.position.y, 0f);
			}
		}else if(Input.GetKeyDown (KeyCode.D)){
			if (myGridPosition + 1 < gm.gridPositions.Length) {
				myGridPosition += 1;
				transform.position = new Vector3 (gm.gridPositions [myGridPosition], transform.position.y, 0f);
			}	
		}

		//movement while key pressed
		if (Input.GetKey (KeyCode.A)) {
			movingTimer += Time.deltaTime;
			if (myGridPosition - 1 > 0 && movingTimer >= speedWhilePressing) {
				myGridPosition -= 1;
				transform.position = new Vector3 (gm.gridPositions [myGridPosition], transform.position.y, 0f);
				movingTimer = 0;
			}
		}else if(Input.GetKey (KeyCode.D)){
			movingTimer += Time.deltaTime;
			if (myGridPosition + 1 < gm.gridPositions.Length && movingTimer >= speedWhilePressing) {
				myGridPosition += 1;
				transform.position = new Vector3 (gm.gridPositions [myGridPosition], transform.position.y, 0f);
				movingTimer = 0;
			}	
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			isFlat = !isFlat;
			ChangePlayerForm ();
		}
	}

//	void FixedUpdate(){
//		//transform.position += moveVelocity;
//		if (transform.position.x < -screenWidth)
//			transform.position = new Vector3 (-screenWidth,transform.position.y,0);
//		if(transform.position.x > screenWidth)
//			transform.position = new Vector3 (screenWidth,transform.position.y,0);
//	}

	public void Die(){
		if (rotatedPlayer.gameObject.activeSelf)
			ChangePlayerForm ();
		Destroy (gameObject);
		gm.GameOver ();
	}

	public void ChangePlayerForm(){
		flatPlayer.gameObject.SetActive (isFlat);
		rotatedPlayer.gameObject.SetActive (!isFlat);
		if(rotatedPlayer.gameObject.activeSelf)
			mySprite = rotatedPlayer.GetComponent<SpriteRenderer>();
		else
			mySprite = flatPlayer.GetComponent<SpriteRenderer>();
		screenWidth = gm.backgroundPause.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2 - mySprite.sprite.bounds.size.x/2;
	}
}
