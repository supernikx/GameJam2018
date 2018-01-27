﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;
    public int life = 100;

	Transform flatPlayer, rotatedPlayer;
	SpriteRenderer mySprite;
	float screenWidth;
	Vector3 moveVelocity;
	bool isFlat = true;

	void Awake(){
		flatPlayer = transform.GetChild (0);
		rotatedPlayer = transform.GetChild (1);
		mySprite = flatPlayer.GetComponent<SpriteRenderer>();
		screenWidth = Camera.main.aspect * Camera.main.orthographicSize - mySprite.sprite.bounds.size.x/2;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        life--;
        Destroy(other.gameObject);
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
