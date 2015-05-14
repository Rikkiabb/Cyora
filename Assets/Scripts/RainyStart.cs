using UnityEngine;
using System.Collections;

public class RainyStart : MonoBehaviour {

	public static bool started = false;


	void OnTriggerEnter2D(Collider2D obj){

		if (obj.name == "Player") {
			started = true;
		}
	}
}
