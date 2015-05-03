using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D obj){

		if (obj.name == "Player") {
			Debug.Log("Player has finished!");
		}
	}
}
