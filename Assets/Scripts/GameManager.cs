using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class GameManager: MonoBehaviour {
    
	[FMODUnity.EventRef]
	public string atmos = null;

    private GameObject player;
    private Vector3 respawnPoint;
    private int deaths = 0;
    private static GameManager gameManager;
    public static GameManager instance  
    {
        get
        {
            if (!gameManager) 
            {
				gameManager = FindObjectOfType (typeof (GameManager)) as GameManager;
                if (!gameManager)
                {
                    Debug.LogError("Need an active GameManager on a GameObject in your scene");
                }
                else 
                {
                    gameManager.Init();
                }
            }
            return gameManager;
        }
    }

    void Init() 
    {
        Debug.Log("init gamemanager");
    }

    void Start() {
        player = FindObjectOfType<PlayerController>().gameObject;

        EventManager.StartListening(Constants.EVENT_PLAYER_HIT, PlayerHit);
        EventManager.StartListening(Constants.EVENT_PLAYER_DIE, PlayerDied);
        EventManager.StartListening(Constants.EVENT_NPC_DIE, NPCDied);

        EventManager.StartListening(Constants.EVENT_CHANGE_RESPAWN_POINT, ChangeRespawnPoint);

		FMOD.Studio.EventInstance atmosphere;

		atmosphere = FMODUnity.RuntimeManager.CreateInstance (atmos);

		atmosphere.start ();

    }

    void PlayerHit(Hashtable h) {
        Debug.Log("player hit");
    }

    void PlayerDied(Hashtable h) {
        Debug.Log("player died");

        player.transform.position = respawnPoint;
        player.GetComponent<PlayerController>().Reset();
        deaths ++;

    }

    void NPCDied(Hashtable h) {
        Debug.Log("npc died");
    }

    void ChangeRespawnPoint(Hashtable h) {
        respawnPoint = EventSerializer.GetRespawnPoint(h);
    }


}