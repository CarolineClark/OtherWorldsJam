using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

	Transform player = null;
	CharacterController characterController;
	public float speed;
    public float rotationSpeed = 1f;

	void Start () {
		characterController = GetComponent<CharacterController>();	
	}
	
	void Update () {
		if (player == null) {
			return;
		}
        
		Vector3 direction = Vector3.Normalize(transform.position - player.position);
		characterController.Move(-1 * direction * speed);

        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime,
                0f
            ), Vector3.forward
        );

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(0, 0, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);
    }

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == Constants.PLAYER_TAG) {
			player = collider.transform;
		}
	}

	public void Kill() {
		Destroy(gameObject);
	}
}
