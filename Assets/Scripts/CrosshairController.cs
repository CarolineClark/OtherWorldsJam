using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

	public float speed = 1;
	CharacterController characterController;
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		float horizontal = Input.GetAxis(Constants.CROSSHAIR_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.CROSSHAIR_VERTICAL_INPUT) * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));
		CheckIfLaserHitsAnything();
	}

	void CheckIfLaserHitsAnything() {
		if (Input.GetButtonDown(Constants.CROSSHAIR_LASER_INPUT)) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hit) && hit.transform != null) {
				GameObject other = hit.transform.gameObject;
				if (other.tag == Constants.NPC_TAG) {
					other.GetComponent<NPCTrigger>().Kill();
				} else if (other.tag == Constants.PLAYER_TAG) {
					other.GetComponent<PlayerController>().Kill();
				}
			}
		}
	}
 }
