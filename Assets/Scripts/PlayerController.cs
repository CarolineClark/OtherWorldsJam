using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1;
    public float rotationSpeed = 1;

	CharacterController characterController;
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		float horizontal = Input.GetAxis(Constants.PLAYER_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.PLAYER_VERTICAL_INPUT) * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));

        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(
                transform.forward,
                new Vector3(horizontal, vertical, 0),
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
