using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LmaoScript : MonoBehaviour {

	VideoPlayer player;

	// Use this for initialization
	void Start () {
		player = GetComponent<VideoPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.isPlaying)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
