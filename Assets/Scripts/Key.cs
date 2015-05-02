using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.name == "Player") {
			Destroy(gameObject);
		}
	}
}
