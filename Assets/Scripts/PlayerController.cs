using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1;
	CharacterController characterController;
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		float horizontal = Input.GetAxis(Constants.PLAYER_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.PLAYER_VERTICAL_INPUT) * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));
	}

	public void Kill() {
		Debug.Log("Player killed!");
	}
}
