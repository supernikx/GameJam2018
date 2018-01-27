using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour {

	public Sprite buttonPressedSprite;
    bool isClicked;
	SpriteRenderer mySpriteRenderer;
	ObstacleButton parentObstacle;

	void Awake(){
        isClicked = false;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		parentObstacle = GetComponentInParent<ObstacleButton> ();
	}

	void OnMouseDown(){
        if (!isClicked)
        {
            parentObstacle.hp--;
            mySpriteRenderer.sprite = buttonPressedSprite;
        }
	}
}
