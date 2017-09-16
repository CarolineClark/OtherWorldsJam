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
		float horizontal = Input.GetAxis("HorizontalController") * speed;
		float vertical = Input.GetAxis("VerticalController") * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));
	}

	
}
