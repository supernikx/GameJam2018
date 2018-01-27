using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour {

	PlayerController playerScript;
	void Awake(){
		playerScript = GetComponentInParent<PlayerController> ();
	}

	void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "obstacle")
            playerScript.Die();
	}
}
