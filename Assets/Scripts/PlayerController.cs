using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1;
    public float rotationSpeed = 1;
    public float hits = 0;
    public float lives = 2;
    public float invinciblityTime = 2f;
    private float invincibleTimeLeft = 0f;
    private bool hasLevelEnded = false;

    CharacterController characterController;
	Animator animator;
	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
        Reset();
        EventManager.StartListening(Constants.EVENT_END_LEVEL, HandleEndLevel);
	}
	
	void Update () {
        if (hasLevelEnded) {
            return;
        }

		float horizontal = Input.GetAxis(Constants.PLAYER_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.PLAYER_VERTICAL_INPUT) * speed;

		Vector3 velocity = new Vector3(horizontal, vertical, 0);
		animator.SetBool(Constants.ANIMATION_TRANSITION_MOVE, (velocity.magnitude > 0));
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

        if (invincibleTimeLeft > 0) {
            CountDownInvincibility();
        }
    }

    public void Reset()
    {
        hits = 0;
        invincibleTimeLeft = invinciblityTime;
    }

    public void Kill(bool isHeatRayDeath = false) {
        if (invincibleTimeLeft > 0) {
            return;
        }

        hits ++;
        invincibleTimeLeft = invinciblityTime;
        EventManager.TriggerEvent(Constants.EVENT_PLAYER_HIT);

        if (hits >= lives) {
            EventManager.TriggerEvent(Constants.EVENT_PLAYER_DIE);

            if (isHeatRayDeath) {
                DeathManager.GetHeatRayDeath().GetComponent<HeatRayDeath>().Init(transform.position);
            }
            else {
                DeathManager.GetNormalDeath().GetComponent<NormalDeath>().Init(transform.position);
            }
        }
	}

    private void CountDownInvincibility()
    {
        invincibleTimeLeft -= Time.deltaTime;
        if (invincibleTimeLeft < 0) {
            invincibleTimeLeft = 0;
        }
    }

    private void HandleEndLevel(Hashtable h)
    {
        hasLevelEnded = true;
    }
}
