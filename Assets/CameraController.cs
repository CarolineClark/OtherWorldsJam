using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject player;
	Vector3 offset = new Vector3(0, 0, -10);
	float shake;
	public float decreaseFactor = 2f;
	public float magnitude = 0.5f;

	void Start () {
		player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
		EventManager.StartListening(Constants.EVENT_NPC_DIE, ScreenShakeListener);
	}
	
	void Update () {
		transform.position = player.transform.position + offset;

		if (shake > 0) {
			transform.localPosition = Random.insideUnitSphere * shake + player.transform.position + offset;
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0.0f;
		}
	}

	void ScreenShakeListener(Hashtable h) {
		ScreenShake();
	}

	void ScreenShake() {
		shake = magnitude;
	}
}
