using UnityEngine;
using System.Collections;

public class BoxMovement : MonoBehaviour {

	// Use this for initialization
	public bool forward;
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Horizontal");

		if (forward) {
			if(move < 0){
				GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);
				GetComponent<Rigidbody2D> ().mass = 100;
			} else {
				GetComponent<Rigidbody2D> ().mass = 1;
			}
		} else {
			if(move > 0){
				GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);
				GetComponent<Rigidbody2D> ().mass = 100;
				return;
			} else {
				GetComponent<Rigidbody2D> ().mass = 1;
			}
		
		}
	}
}
