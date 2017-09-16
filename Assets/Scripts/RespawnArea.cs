using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour {

    private Vector3 respawnPoint;

	public void Start ()
    {
        respawnPoint = GetComponentInChildren<RespawnPoint>().transform.position;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.PLAYER_TAG) {
            EventManager.TriggerEvent(
                Constants.EVENT_CHANGE_RESPAWN_POINT, 
                EventSerializer.CreateRespawnPoint(respawnPoint)
            );
        }
    }
}
