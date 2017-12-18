using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		// Destroy(gameObject, )
	}

	// Update is called once per frame
	void Update () {
		Vector2 moveDirection = rb.velocity;
		if(moveDirection != Vector2.zero){
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.Rotate(0,0,-90);
		}
	}
}
