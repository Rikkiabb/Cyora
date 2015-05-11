using UnityEngine;
using System.Collections;


public class BossAI : MonoBehaviour {

	// Create a new player stat class which handles his health and weapons
	[System.Serializable]
	public class BossStats {
		public int Health = 3;
		public int attackHit = 1;
	}

	public BossStats stats = new BossStats();
	public Transform Player;
	public Transform transformEnergyBall;
	Rigidbody2D energyBall;
	float distanceX;
	float distanceY;
	float lookAtDistance = 10f;
	float followDistance = 20f;
	float speed = 7f;
	float ballSpeed = 17f;
	public bool canHurt = true;
	bool canShoot = true;
	public bool isHurt = false;
	int startHealth;
	EnemyHealth eh;
	Animator anim;




	// Use this for initialization
	void Start () {
		energyBall = transformEnergyBall.gameObject.GetComponent<Rigidbody2D> ();
		eh = gameObject.GetComponentInChildren<EnemyHealth>();
		startHealth = stats.Health;
		anim = GetComponent<Animator> ();
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

	public void DamageBoss (int damage){
		if (isHurt) {
			return;
		}
		isHurt = true;
		StartCoroutine (WaitHurt ());
		stats.Health -= damage;
		
		// So if our player empties his health he dies
		if(stats.Health <= 0){
			Debug.Log("Kill Enemy!!");
			Destroy (transform.parent.gameObject);
			return;
		}
		
		anim.SetBool ("IsHurt", true);
		
		float damagePercent = (float)stats.Health / startHealth;

		eh.UpdateHealthBar(damagePercent);
	}


	IEnumerator WaitEnergyBall(){
		yield return new WaitForSeconds(2);
		canShoot = true;
	}

	public IEnumerator WaitHurt(){
		yield return new WaitForSeconds(2);
		isHurt = false;
		anim.SetBool ("IsHurt", false);
	}
}
