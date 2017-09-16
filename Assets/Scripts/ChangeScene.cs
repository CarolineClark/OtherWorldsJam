using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    public GameObject nextSceneButton;

    public void Update()
    {
        if (Input.GetButtonDown(Constants.CROSSHAIR_LASER_INPUT)) {
            nextSceneButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void NewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
