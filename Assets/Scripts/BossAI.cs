using UnityEngine;
using System.Collections;


public class BossAI : MonoBehaviour {

	public Transform Player;
	float distanceX;
	float distanceY;
	float lookAtDistance = 10f;
	float followDistance = 20f;
	float speed = 10f;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		distanceX = Player.position.x - transform.position.x;
		distanceY = Player.position.y - transform.position.y;

		if (Mathf.Abs(distanceX)< lookAtDistance) {
			LookAt ();
		}

		if (Mathf.Abs(distanceX) < followDistance && Mathf.Abs(distanceY) < followDistance) {
			FollowX();
			FollowY();
		}



	}

	void LookAt(){
		Quaternion temp = transform.rotation;

		if (distanceX > 0) {
			temp.y = -180;
		} else {
			temp.y = 0;
		}

		transform.rotation = temp;
	}

	void FollowX(){
		if (Mathf.Abs (distanceX) < 1.1) {
			Debug.Log (distanceY);
			return;
		}

		transform.Translate (Vector3.left * speed * Time.deltaTime);

	}

	void FollowY(){

		if (Mathf.Abs (distanceY) < 1.1) {
			return;
		}

		if (distanceY > 0) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		}
		
	}
}
