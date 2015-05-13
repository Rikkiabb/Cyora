using UnityEngine;
using System.Collections;


public class MovingPlatform : MonoBehaviour {
	
	public bool forward = false;
	public float speed = 5f;
	public bool leftToRight = true;
	int timer = 0;

	public int maxTimer = 10;
	

	// Update is called once per frame
	void Update () {
		//
		
		
		if (forward) {
			
			if (leftToRight) {

				GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed*Time.deltaTime, 0) ;
			
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed*Time.deltaTime) ;
			}

		} else {
			
			if (leftToRight) {
				
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed*Time.deltaTime, 0) ;
				
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed*Time.deltaTime) ;
			}			
		}
		
		Debug.Log (Time.time);
		timer++;
		if (timer == maxTimer) {
			timer = 0;
//			Time.time;
			forward = !forward;
		}
		
		
	}

	void OnCollisionStay2D(Collision2D coll){
		
		if (coll.gameObject.tag == "Player") {

			PlayerScript player = coll.gameObject.GetComponent<PlayerScript> ();

			if (forward) {
				
				if (leftToRight) {

					player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (speed * player.maxSpeed * 3f, 0));

				}
			} else {
				
				if (leftToRight) {

					player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-speed * player.maxSpeed * 3f, 0));
				}
			}

				
		} 

	}
}