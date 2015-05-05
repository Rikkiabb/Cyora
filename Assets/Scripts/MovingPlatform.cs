using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	
	public bool forward = false;
	public float speed = 3f;
	public bool leftToRight = true;
	int timer = 0;
	public int maxTimer = 10;
	Rigidbody2D rig;

	void Start(){
		rig = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
	

		if (forward) {

//			if (leftToRight) {
				//GetComponent<Rigidbody2D> ().velocity.x = speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, 0) ;

//			rig.velocity.x = speed;

//			} else {
//				//GetComponent<Rigidbody2D> ().velocity.y = speed;
//				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, speed));
//			}
		} else {
		
//			if (leftToRight) {
				//GetComponent<Rigidbody2D> ().velocity.x = -speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed, 0) ;
//			} else {
//				//GetComponent<Rigidbody2D> ().velocity.y = -speed;
//				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -speed));
//			}
		}


		timer++;
		if (timer == maxTimer) {
			timer = 0;
			forward = !forward;
		}


	}
}
