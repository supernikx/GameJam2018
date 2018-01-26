using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;
	SpriteRenderer mySprite;

	float screenWidth;
	Vector3 moveVelocity;

	void Awake(){
		mySprite = GetComponent<SpriteRenderer> ();
		screenWidth = Camera.main.aspect * Camera.main.orthographicSize - mySprite.sprite.bounds.size.x/2;
	}
		
	void Update () {
		Vector3 inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, 0f).normalized;
		moveVelocity = inputDirection * moveSpeed * Time.deltaTime;
	}

	void FixedUpdate(){
		transform.position += moveVelocity;
		if (transform.position.x < -screenWidth)
			transform.position = new Vector3 (-screenWidth,transform.position.y,0);
		if(transform.position.x > screenWidth)
			transform.position = new Vector3 (screenWidth,transform.position.y,0);
	}
}
