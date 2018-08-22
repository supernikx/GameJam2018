using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

	public RawImage image;
	VideoPlayer player;
	public VideoClip source;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		StartCoroutine (PlayVideo());
	}

	public IEnumerator PlayVideo(){
		player = gameObject.AddComponent<VideoPlayer> ();
		player.playOnAwake = false;
		player.clip = source;
		player.Prepare ();
		image.texture = player.texture;
		player.Play ();
		yield return null;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
