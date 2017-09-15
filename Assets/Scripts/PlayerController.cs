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
		float horizontal = Input.GetAxis("Horizontal") * speed;
		float vertical = Input.GetAxis("Vertical") * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));
	}
}
