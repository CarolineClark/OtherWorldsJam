using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntBuilding : MonoBehaviour {
	public Mesh BurntMesh;

	public void Start()
	{
	}

	public void Burn ()
	{
		GetComponent<MeshFilter> ().mesh = BurntMesh;
	}

}
