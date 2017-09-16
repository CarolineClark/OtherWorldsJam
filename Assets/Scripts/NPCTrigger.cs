using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

	Transform player = null;
	CharacterController characterController;
	public float speed;
    public float rotationSpeed = 1f;
    public float closeDistance = 0.1f;
    private Vector3 startPos;

	void Start () {
        startPos = transform.position;
		characterController = GetComponent<CharacterController>();
        EventManager.StartListening(Constants.EVENT_PLAYER_DIE, PlayerDied);
	}
	
	void Update () {
		if (player == null && Vector3.Distance(transform.position, startPos) < closeDistance) {
			return;
		}

        Vector3 targetPos = player != null ? player.position : startPos;
        
		Vector3 direction = Vector3.Normalize(transform.position - targetPos);
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
		EventManager.TriggerEvent(Constants.EVENT_NPC_DIE);
		Destroy(gameObject);
	}

    public void PlayerDied(Hashtable hash)
    {
        player = null;
    }
}
