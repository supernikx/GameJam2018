using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour {

	public Sprite buttonPressedSprite;

	SpriteRenderer mySpriteRenderer;
	ObstacleButton parentObstacle;

	void Awake(){
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		parentObstacle = GetComponentInParent<ObstacleButton> ();
	}

	void OnMouseDown(){
		parentObstacle.hp--;
		mySpriteRenderer.sprite = buttonPressedSprite;
	}
}
