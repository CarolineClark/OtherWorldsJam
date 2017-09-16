using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class GameManager: MonoBehaviour {

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
        EventManager.StartListening(Constants.EVENT_PLAYER_HIT, PlayerHit);
        EventManager.StartListening(Constants.EVENT_PLAYER_DIE, PlayerDied);
        EventManager.StartListening(Constants.EVENT_NPC_DIE, NPCDied);
    }

    void PlayerHit(Hashtable h) {
        Debug.Log("player hit");
    }

    void PlayerDied(Hashtable h) {
        Debug.Log("player died");
    }

    void NPCDied(Hashtable h) {
        Debug.Log("npc died");
    }


}