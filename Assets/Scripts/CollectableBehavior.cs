using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableBehavior : MonoBehaviour {

	GameObject parentObject;
	CollectableController parentController;
	public Image collectablePanelImage;
	public int collectableNumber;
	Color tempColor;

	// Use this for initialization
	void Start () {
		parentObject = transform.parent.gameObject;
		parentController = parentObject.GetComponent<CollectableController>();
		tempColor = new Color(1,1,1,1);
		// tempColor.a = 1.0f;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(){
		print("Triggered!");
		parentController.collected[collectableNumber] = true;
		collectablePanelImage.color = tempColor;
		Destroy(gameObject);
	}
}
