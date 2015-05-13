using UnityEngine;
using System.Collections;


public class MovingPlatform : MonoBehaviour {
	
	public bool forward = false;
	public float speed = 5f;
	public bool leftToRight = true;
	int timer = 0;

	public float maxTimer = 10f;

	void Start(){
		StartCoroutine (WaitForward (maxTimer));
	}

	// Update is called once per frame
	void Update () {
		//
		
		
		if (forward) {
			
			if (leftToRight) {

				GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, 0) ;
			
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed) ;
			}

		} else {
			
			if (leftToRight) {
				
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed, 0) ;
				
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed) ;
			}			
		}
		
//		Debug.Log (Time.time);
//		timer++;
//		if (timer == maxTimer) {
//			timer = 0;
////			Time.time;
//			forward = !forward;
//		}
		
		
	}

	IEnumerator WaitForward(float waitTime){

		yield return new WaitForSeconds (waitTime);
		forward = !forward;
		StartCoroutine (WaitForward (waitTime));
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