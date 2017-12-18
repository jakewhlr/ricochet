using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {

	public bool [] collected;
	// Use this for initialization
	void Start () {
		collected = new bool[3];
		collected[0] = false;
		collected[1] = false;
		collected[2] = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
