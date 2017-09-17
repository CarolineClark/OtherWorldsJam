using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBarFill : MonoBehaviour {
	Image image;

	void Start () {
		image = GetComponent<Image>();
		image.type = Image.Type.Filled;
		image.fillMethod = Image.FillMethod.Horizontal;
		image.fillOrigin = (int)Image.OriginHorizontal.Left;
		image.fillAmount = 1;
	}
	
	public void ShowPercentageOfElement(float percentage) {
		image.fillAmount = percentage;
	}
}
