using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public Transform keyEffect;

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.name == "Player") {
			Instantiate(keyEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
