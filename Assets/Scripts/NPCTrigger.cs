using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

	Transform player = null;
	CharacterController characterController;
	public float speed;

	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		if (player == null) {
			return;
		}

		Vector3 diff = Vector3.Normalize(transform.position - player.position);
		characterController.Move(-1 * diff * speed);
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == Constants.PLAYER_TAG) {
			player = collider.transform;
		}
	}
}
