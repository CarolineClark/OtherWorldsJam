using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

	public float speed = 1;
    private float laserReductionSpeed = 0.3f;
    private float laserChargingSpeed = 0.15f;
    public GameObject burn;
    private GameObject[] burns;
    private int burnPos = 0;
    public int maxBurns = 1000;
    private bool breakInBurn = true;
    private Vector3 lastPoint;
    private Camera cam;
    private Vector3 oldCamPos;
	CharacterController characterController;
    private bool hasLevelEnded = false;
    private float maxLaser = 1;
    private float minLaser = 0;
    private float laserLeft;
    LaserBarFill laserBarUi;

    void Start () {
        laserLeft = maxLaser;
		characterController = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
        oldCamPos = cam.transform.position;
        laserBarUi = GameObject.FindGameObjectWithTag(Constants.UI_LASER_BAR).GetComponent<LaserBarFill>();
        EventManager.StartListening(Constants.EVENT_END_LEVEL, HandleEndLevel);
        InitBurns();
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
        }

        LaserLogic();
    }

    void LaserLogic()
    {
        if (Input.GetButton(Constants.CROSSHAIR_LASER_INPUT)) {
            laserLeft -= laserReductionSpeed * Time.deltaTime;
            if (laserLeft > 0) {
                CheckIfLaserHitAnything();
            }

        }
        else {
            laserLeft += laserChargingSpeed * Time.deltaTime;
            breakInBurn = true;
        }
        laserBarUi.ShowPercentageOfElement(laserLeft);
	}

    void CheckIfLaserHitAnything() {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.Normalize(cam.transform.position - transform.position) * -10);
        bool hasBurned = false;
        foreach (RaycastHit hit in hits) {
            if (hit.transform == null) {
                continue;
            }

            GameObject other = hit.transform.gameObject;
            if (other.tag == Constants.BURNABLE_TAG && !hasBurned) {
                if (breakInBurn) {
                    lastPoint = hit.point;
                    breakInBurn = false;
                }
                AddBurn(lastPoint, hit.point);
                lastPoint = hit.point;
                hasBurned = true;
            }

            if (other.tag == Constants.NPC_TAG) {
                other.GetComponent<NPCTrigger>().Kill(true);
            }
            else if (other.tag == Constants.PLAYER_TAG) {
                other.GetComponentInParent<PlayerController>().Kill(true);
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

    private void InitBurns()
    {
        burns = new GameObject[maxBurns];
        for (int i = 0; i < maxBurns; i++) {
            burns[i] = Instantiate(burn);
        }


    }

    private GameObject GetNextBurn()
    {
        burnPos++;
        if (burnPos >= maxBurns) {
            burnPos = 0;
        }

        return burns[burnPos];
    }

    private void AddBurn(Vector3 oldPos, Vector3 newPos)
    {
        if (Vector3.Distance(oldPos, newPos) > 1f) {
            return;
        }

        float howOften = 0.04f;
        Vector3 dir = Vector3.Normalize(newPos - oldPos) * howOften;
        int count = Mathf.CeilToInt(Vector3.Distance(oldPos, newPos) / howOften);

        for (int i = 0; i < count; i++) {
            GameObject burn = GetNextBurn();
            burn.transform.position = oldPos + (dir * i);
            burn.GetComponent<Burn>().Init();
        }
    }
}
