using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour {

    public GameObject endScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.PLAYER_TAG) {
            Time.timeScale = 0;
            endScreen.SetActive(true);
            EventManager.TriggerEvent(Constants.EVENT_END_LEVEL);
        }
    }
}
