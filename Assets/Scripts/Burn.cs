using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour {

    private SpriteRenderer sprite;
    private Light ourLight;
    private bool changeToWhite = true;
    private bool changeToOrange = false;
    private bool changeToRed = false;
    private bool changeToBlack = false;

    public void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

	public void Init () {
        gameObject.SetActive(true);
        ourLight = GetComponentInChildren<Light>();
        ourLight.enabled = false;

        StartCoroutine(Kill());

        if (Random.Range(0, 10) > 8.9) {
            ourLight.enabled = true;
        }

        changeToWhite = true;
        changeToOrange = false;
        changeToRed = false;
        changeToBlack = false;
    }

    public void Update()
    {
        Color white = new Color(1f, 0.9f, 0.7f, 0.2f);
        Color red = new Color(0.8f, 0.1f, 0.1f, 0.2f);
        Color orange = new Color(0.6f, 0.4f, 0.1f, 0.2f);
        Color black = new Color(0.2f, 0.1f, 0.1f, 0.2f);
        
        ourLight.intensity = Mathf.Lerp(3f, 0.2f, Time.deltaTime / 0.02f);

        if (changeToWhite) {
            sprite.color = white;
        }
        if (changeToOrange) {
            sprite.color = Color.Lerp(white, orange, Time.deltaTime / 0.015f);
        }
        if (changeToRed) {
            sprite.color = Color.Lerp(orange, red, Time.deltaTime / 0.015f);
        }
        if (changeToBlack) {
            sprite.color = Color.Lerp(red, black, Time.deltaTime / 0.015f);
        }
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(0.2f);
        changeToWhite = false;
        changeToOrange = true;
        yield return new WaitForSeconds(0.15f);
        changeToOrange = false;
        changeToRed = true;
        yield return new WaitForSeconds(0.15f);
        changeToRed = false;
        changeToBlack = true;
        yield return new WaitForSeconds(0.15f);

        gameObject.SetActive(false);
    }
}
