using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1;
    public float rotationSpeed = 1;

	CharacterController characterController;
	Animator animator;
	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		float horizontal = Input.GetAxis(Constants.PLAYER_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.PLAYER_VERTICAL_INPUT) * speed;

		Vector3 velocity = new Vector3(horizontal, vertical, 0);
		animator.SetBool("move", (velocity.magnitude > 0));
		characterController.Move(velocity);

        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(
                transform.forward,
                velocity,
                rotationSpeed * Time.deltaTime,
                0f
            ), Vector3.forward
        );

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(0, 0, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);
	}

	public void Kill() {
		EventManager.TriggerEvent(Constants.EVENT_PLAYER_DIE);
	}
}
