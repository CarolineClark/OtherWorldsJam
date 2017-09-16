using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

	public float speed = 1;
    private Camera cam;
    private Vector3 oldCamPos;
	CharacterController characterController;
    private bool hasLevelEnded = false;

    void Start () {
		characterController = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
        oldCamPos = cam.transform.position;
        EventManager.StartListening(Constants.EVENT_END_LEVEL, HandleEndLevel);
    }
	
	void Update () {
        if (hasLevelEnded) {
            return;
        }
        
        float horizontal = Input.GetAxis(Constants.CROSSHAIR_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.CROSSHAIR_VERTICAL_INPUT) * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));

        KeepCrosshairOnScreen();
        
        CheckIfLaserHitsAnything();
    }

	void CheckIfLaserHitsAnything() {
		if (Input.GetButton(Constants.CROSSHAIR_LASER_INPUT)) {
            
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.Normalize(cam.transform.position - transform.position) * -10);
            foreach (RaycastHit hit in hits) {
                if (hit.transform == null) {
                    continue;
                }

                GameObject other = hit.transform.gameObject;
                if (other.tag == Constants.NPC_TAG) {
                    other.GetComponent<NPCTrigger>().Kill(true);
                }
                else if (other.tag == Constants.PLAYER_TAG) {
                    other.GetComponentInParent<PlayerController>().Kill(true);
                }
            }
		}
	}

    void KeepCrosshairOnScreen() {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 newPos = transform.position;
        if (screenPos.x < 0) {
            newPos.x = cam.ScreenToWorldPoint(new Vector3(0, screenPos.y, screenPos.z)).x;
        }
        if (screenPos.x > Screen.width) {
            newPos.x = cam.ScreenToWorldPoint(new Vector3(Screen.width, screenPos.y, screenPos.z)).x;
        }

        if (screenPos.y < 0) {
            newPos.y = cam.ScreenToWorldPoint(new Vector3(screenPos.x, 0, screenPos.z)).y;
        }
        if (screenPos.y > Screen.height) {
            newPos.y = cam.ScreenToWorldPoint(new Vector3(screenPos.x, Screen.height, screenPos.z)).y;
        }

        newPos += cam.transform.position - oldCamPos;
        oldCamPos = cam.transform.position;

        transform.position = newPos;
    }

    private void HandleEndLevel(Hashtable h)
    {
        hasLevelEnded = true;
    }
}
