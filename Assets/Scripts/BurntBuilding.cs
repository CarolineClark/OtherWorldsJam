using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntBuilding : MonoBehaviour {
	//public Mesh BurntMesh;
	public GameObject BurntHouse;

	public void Start()
	{
	}

	public void Burn ()
	{
//		GetComponent<MeshFilter> ().mesh = BurntMesh;
		Instantiate(BurntHouse, transform.position, transform.rotation);
		Destroy (gameObject);
		Debug.Log ("Burn is called");
	}

}
