﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

	public float speed = 1;
    private float laserReductionSpeed = 0.3f;
    private float laserChargingSpeed = 0.15f;
    private Camera cam;
    private Vector3 oldCamPos;
	CharacterController characterController;
    private bool hasLevelEnded = false;
    private float maxLaser = 1;
    private float minLaser = 0;
    private float laserLeft;
    LaserBarFill laserBarUi;

	[FMODUnity.EventRef]
	public string lasorFire = null;
	private FMOD.Studio.EventInstance alienLasor;

	bool lasorOn = false;
	bool laserOff = false;


    void Start () {
        laserLeft = maxLaser;
		characterController = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
        oldCamPos = cam.transform.position;
        laserBarUi = GameObject.FindGameObjectWithTag(Constants.UI_LASER_BAR).GetComponent<LaserBarFill>();
        //laser = GetComponentInChildren<ParticleLauncher>();
        EventManager.StartListening(Constants.EVENT_END_LEVEL, HandleEndLevel);

		alienLasor = FMODUnity.RuntimeManager.CreateInstance (lasorFire);
		alienLasor.setParameterValue ("buttonDown", 1);

    }
	
	void Update () {
        if (hasLevelEnded) {
            return;
        }
        
        float horizontal = Input.GetAxis(Constants.CROSSHAIR_HORIZONTAL_INPUT) * speed;
		float vertical = Input.GetAxis(Constants.CROSSHAIR_VERTICAL_INPUT) * speed;
		characterController.Move(new Vector3(horizontal, vertical, 0));

        KeepCrosshairOnScreen();
        
		if (laserLeft <= 0) {
			laserLeft = 0;
			//alienLasor.setParameterValue ("buttonHold", 1);
		} 
		else if (laserLeft == 1) 
		{
			lasorOn = false;
		}

        LaserLogic();
    }

	void LaserLogic() {

		if (Input.GetButton (Constants.CROSSHAIR_LASER_INPUT)) 
		{
			laserLeft -= laserReductionSpeed * Time.deltaTime;

			if (laserLeft > 0) {

				if (lasorOn == false) {
					Debug.Log ("ON");
					lasorOn = true;
					alienLasor.setParameterValue ("buttonDown", 1);
					alienLasor.start ();
				}
				CheckIfLaserHitAnything ();
				//alienLasor.setParameterValue ("buttonDown", 1);

			} 
			else 
			{
				alienLasor.setParameterValue ("buttonDown", 0);
				Debug.Log ("OFF");
			}
		}
		else 
		{
			alienLasor.setParameterValue ("buttonDown", 0);
            laserLeft += laserChargingSpeed * Time.deltaTime;
			Debug.Log ("Off");
			lasorOn = false;
        }
        
		laserBarUi.ShowPercentageOfElement(laserLeft);
	}

	//---------------------------------Audio---------------------------------//
    void CheckIfLaserHitAnything() {
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
	//---------------------------------Audio---------------------------------//


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
