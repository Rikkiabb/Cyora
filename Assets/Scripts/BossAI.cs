using UnityEngine;
using System.Collections;


public class BossAI : MonoBehaviour {

	public Transform Player;
	public Transform transformEnergyBall;
	Rigidbody2D energyBall;
	float distanceX;
	float distanceY;
	float lookAtDistance = 10f;
	float followDistance = 20f;
	float speed = 7f;
	float ballSpeed = 17f;
	bool canShoot = true;




	// Use this for initialization
	void Start () {
		energyBall = transformEnergyBall.gameObject.GetComponent<Rigidbody2D> ();
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
			return;
		}

		transform.Translate (Vector3.left * speed * Time.deltaTime);

	}

	void FollowY(){

		if (Mathf.Abs (distanceY) < 1.1) {

			if(canShoot){
				Rigidbody2D ball = Instantiate(energyBall, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				if(distanceX > 0){
					ball.velocity = new Vector2(ballSpeed, 0);

				} else{
					ball.velocity = new Vector2(-ballSpeed, 0);

				}
				canShoot = false;
				StartCoroutine(WaitEnergyBall());
			}

			return;
		}

		if (distanceY > 0) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		}
		
	}

	IEnumerator WaitEnergyBall(){
		yield return new WaitForSeconds(2);
		canShoot = true;
	}
}
