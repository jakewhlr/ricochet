using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehavior : MonoBehaviour {

	public Canvas LevelCompleteCanvas;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(){
		LevelCompleteCanvas.gameObject.SetActive(true);
		Destroy(gameObject);		
	}
}
