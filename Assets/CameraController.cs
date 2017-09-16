using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject player;
	Vector3 offset = new Vector3(0, 0, -1);

	void Start () {
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
	}
	
	void Update () {
		transform.position = player.transform.position + offset;
	}
}
