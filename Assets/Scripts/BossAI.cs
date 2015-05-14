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
	public Transform heart;
	Rigidbody2D energyBall;
	float distanceX;
	float distanceY;
	float followDistance = 180f;
	float speed = 4f;
	float ballSpeed = 17f;
	public bool canHurt = true;
	bool canShoot = true;
	public bool isHurt = false;
	int startHealth;
	EnemyHealth eh;
	Animator anim;
	public PhysicsMaterial2D slipperyMat;
	public PhysicsMaterial2D normalMat;




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

		if (Mathf.Abs(distanceX) < followDistance && Mathf.Abs(distanceY) < followDistance) {
			LookAt ();
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
		

		if (stats.Health == 9) {
			GameMasterCS game = GameObject.Find("_GM").GetComponent<GameMasterCS>();
			game.StopFinal("stage2");
			Instantiate(heart, new Vector3(-333.3042f, 898.9985f, 0), Quaternion.identity);
		} 
		else if (stats.Health == 6) {
			GameMasterCS game = GameObject.Find("_GM").GetComponent<GameMasterCS>();
			game.StopFinal("stage3");
			Instantiate(heart, new Vector3(-277.1549f, 898.64485f, 0), Quaternion.identity);
		}
		else if (stats.Health == 3) {
			GameMasterCS game = GameObject.Find("_GM").GetComponent<GameMasterCS>();
			game.StopFinal("stage4");
			Instantiate(heart, new Vector3(-304.4326f, 893.6852f, 0), Quaternion.identity);
		}
		else if(stats.Health <= 0){
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			
			for (int i = 0; i < enemies.Length; i++) {
				Destroy (enemies[i].gameObject);
			}
			GameMasterCS game = GameObject.Find("_GM").GetComponent<GameMasterCS>();
			game.Victory();
			Destroy (transform.parent.gameObject);


			return;
		}

		
		anim.SetBool ("IsHurt", true);
		
		float damagePercent = (float)stats.Health / startHealth;

		eh.UpdateHealthBar(damagePercent);
	}

	public void Stage2(){
		Physics2D.gravity = new Vector2(-115, -30);
	}

	public void Stage3(){
		Physics2D.gravity = new Vector2(0, -30);
		GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");

		for (int i = 0; i < clouds.Length; i++) {
			SpriteRenderer[] parts = clouds[i].GetComponentsInChildren<SpriteRenderer>();

			for(int j = 0; j < parts.Length; j++){
				parts[j].color = new Color(0.839f,1f,1f,1f);			
			}

			CircleCollider2D[] circleColl = clouds[i].GetComponents<CircleCollider2D>();
			BoxCollider2D[] boxColl = clouds[i].GetComponents<BoxCollider2D>();

			for(int j = 0; j < circleColl.Length; j++){

				circleColl[j].sharedMaterial = slipperyMat;
				circleColl[j].enabled = false;
				circleColl[j].enabled = true;
			}

			for(int j = 0; j < boxColl.Length; j++){

				boxColl[j].sharedMaterial = slipperyMat;
				boxColl[j].enabled = false;
				boxColl[j].enabled = true;
			}
		}

		GameMasterCS.setIce (true);
	}

	public void Stage4(){
		GameObject[] clouds = GameObject.FindGameObjectsWithTag ("Cloud");
		
		for (int i = 0; i < clouds.Length; i++) {
			SpriteRenderer[] parts = clouds [i].GetComponentsInChildren<SpriteRenderer> ();
			
			for (int j = 0; j < parts.Length; j++) {
				parts [j].color = new Color (1f, 1f, 1f, 1f);			
			}
			
			CircleCollider2D[] circleColl = clouds [i].GetComponents<CircleCollider2D> ();
			BoxCollider2D[] boxColl = clouds [i].GetComponents<BoxCollider2D> ();
			
			for (int j = 0; j < circleColl.Length; j++) {
				
				circleColl [j].sharedMaterial = normalMat;
				circleColl [j].enabled = false;
				circleColl [j].enabled = true;
			}
			
			for (int j = 0; j < boxColl.Length; j++) {
				
				boxColl [j].sharedMaterial = normalMat;
				boxColl [j].enabled = false;
				boxColl [j].enabled = true;
			}
		}
		
		GameMasterCS.setIce (false);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		for (int i = 0; i < enemies.Length; i++) {
			enemies [i].transform.Find ("Monster44").gameObject.SetActive(true);
		}
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
