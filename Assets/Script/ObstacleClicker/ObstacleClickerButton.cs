using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClickerButton : MonoBehaviour {
    private ObstacleClicker parent;
	AudioSource pressSound;

	void Awake(){
		parent = gameObject.transform.parent.gameObject.GetComponent<ObstacleClicker>();
		pressSound = GameObject.Find ("ButtonPressedAudio").GetComponent<AudioSource>();
	}

    private void OnMouseDown()
    {
		pressSound.Play ();
        parent.ButtonClicked();
    }
}
