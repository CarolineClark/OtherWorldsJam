using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

	[FMODUnity.EventRef]
	public string enemyDeathEvent = null;



	Transform player = null;
	CharacterController characterController;
	public float speed;
    public float rotationSpeed = 1f;
    public float closeDistance = 0.1f;
    private Vector3 startPos;
    private bool hasLevelEnded = false;
    Animator animator;

	void Start () {
        startPos = transform.position;
		characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        EventManager.StartListening(Constants.EVENT_PLAYER_DIE, PlayerDied);
        EventManager.StartListening(Constants.EVENT_END_LEVEL, HandleEndLevel);
    }
	
	void Update () {
		if (hasLevelEnded || (player == null && Vector3.Distance(transform.position, startPos) < closeDistance)) {
			return;
		}

        Vector3 targetPos = player != null ? player.position : startPos;
		Vector3 direction = Vector3.Normalize(transform.position - targetPos);
        Vector3 velocity = -1 * direction * speed;
		characterController.Move(velocity);

        animator.SetBool(Constants.ANIMATION_TRANSITION_MOVE, (velocity.magnitude > 0));

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
		if (collider.tag == Constants.PLAYER_TO_NPC_TAG) {
			player = collider.transform;
		}
	}

	public void Kill(bool isHeatRayDeath = false) {
		EventManager.TriggerEvent(Constants.EVENT_NPC_DIE);

        if (isHeatRayDeath) {
            DeathManager.GetHeatRayDeath().GetComponent<HeatRayDeath>().Init(transform.position);
        }
        else {
            DeathManager.GetNormalDeath().GetComponent<NormalDeath>().Init(transform.position);
        }

		FMODUnity.RuntimeManager.PlayOneShotAttached (enemyDeathEvent, gameObject);
        DestroyImmediate(gameObject);



	}

    public void PlayerDied(Hashtable hash)
    {
        player = null;
    }

    private void HandleEndLevel(Hashtable h)
    {
        hasLevelEnded = true;
    }
}
