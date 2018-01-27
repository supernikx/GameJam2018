using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour {

	public float movePerSecond;

	void Update () {
		transform.position += new Vector3 (transform.position.x, -movePerSecond * Time.deltaTime, 0f);
	}
}
