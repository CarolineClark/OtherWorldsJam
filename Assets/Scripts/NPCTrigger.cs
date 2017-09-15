using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log("hit");
		if (collider.tag == Constants.PLAYER_TAG) {
			Debug.Log("test");
		}
	}
}
