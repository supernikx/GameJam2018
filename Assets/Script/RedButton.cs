using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour {

	public Sprite buttonPressedSprite;
    bool isClicked;
	AudioSource pressSound;
	SpriteRenderer mySpriteRenderer;
	ObstacleButton parentObstacle;

	void Awake(){
        isClicked = false;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		parentObstacle = GetComponentInParent<ObstacleButton> ();
		pressSound = GameObject.Find ("ButtonPressedAudio").GetComponent<AudioSource>();
	}

	void OnMouseDown(){
        if (!isClicked)
        {
			pressSound.Play ();
            parentObstacle.hp--;
            mySpriteRenderer.sprite = buttonPressedSprite;
        }
	}
}
