using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	EnemyHealth eh;
	Animator anim;
	public bool isHurt = false;
	private int startHealth;
	void Start(){
		// Get a reference to the enemy health script
		eh = gameObject.GetComponentInChildren<EnemyHealth>();
		// Get the initial health of the enemy
		startHealth = stats.Health;
		anim = GetComponent<Animator> ();
	}


	// Create a new player stat class which handles his health and weapons
	[System.Serializable]
	public class EnemyStats {
		public int Health = 3;
		public int attackHit = 1;
	}

	//Handles enemy movement.
	[System.Serializable]
	public class EnemyMovement{
		//Variables to be able to move the enemy
		public bool forward = false;
		public float speed = 5f;
		public bool leftToRight = true;
		public int maxTimer = 10;
		public bool wait = false;
		public float waitTime = 2f;
	}

	//For enemy movement.
	int timer = 0;

	// instantiate
	public EnemyStats stats = new EnemyStats();
	public EnemyMovement movement = new EnemyMovement();

	// how much a player will bounce of enemy on contact
	public float bounceAmount = 15f;
	// this update isn't really neccessary for our enemy class but could be useful later on TODO refactor
	void Update(){

		Move ();
	}

	void Move(){

		//only happens if enemy waits a while before tunring around
		if (timer >= movement.maxTimer) {
			return;
		}

		if (movement.forward) {
			
			if (movement.leftToRight) {

				GetComponent<Rigidbody2D> ().velocity = new Vector2 (movement.speed, 0) ;
				
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, movement.speed) ;
			}
			
		} else {
			
			if (movement.leftToRight) {
				
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-movement.speed, 0) ;
				
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -movement.speed) ;
			}			
		}
		
		
		timer++;
		if (timer == movement.maxTimer) {

			if(movement.wait && movement.forward){
				Debug.Log ("WAIT!");
				StartCoroutine(Wait (movement.waitTime));
			}
			else{
				timer = 0;
			}

			movement.forward = !movement.forward;

		}

	}

	// How long before he can occur damage again
	public float repeatDamagePeriod = 2f;	
	// The last time he was hit
	private float lastHitTime;
	// This function gets called whenever something collides with our thingy
//	void OnCollisionEnter2D(Collision2D coll){
//
//		PlayerScript player = coll.gameObject.GetComponent<PlayerScript> ();
//
//		if (coll.gameObject.tag == "Player" && (coll.collider.tag != "Sword" || (coll.collider.tag == "Sword" && !player.isAttacking()))) {
//
//			if (Time.time > lastHitTime + repeatDamagePeriod) {
//				// We need to get the incoming collider that was involved in the collision
//				//TODO: Remove these stupid logs when we build the project
//				// We want to check the vector perpendicular to the surface of the incoming Collider2D at the contact point.
//				Vector2 pointOfContact = coll.contacts [0].normal; //Grab the normal of the contact point we touched
//				Debug.Log (pointOfContact);
//				// Store an instance of the player thad collided with the enemy
//
//
//				// Deal appropriate damage to him 
//				player.DamagePlayer (stats.attackHit);
//				lastHitTime = Time.time;
//
//				// Get the rigidbody of our player so we can manipulate it
//				Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();
//				
//				// What we need to to is find out which side of the collider hit our player and apply force to him
//				// in the opposite direction
//				Vector2 v = rb.velocity;
//			
//				//Detect which side of the collider we touched
//				// So what the normal does is gives us a two dimensional representation of the direction which the player 
//				// is approaching from
//				// A vector(-1,0) means we are coming from the right side when we impact
//				if (player.facingRight) {
//					// Bounce to the left
//					Debug.Log ("We touched the left side of the enemy!");
//					v.y = bounceAmount;
//					rb.velocity = v;
//					rb.AddForce (Vector2.right * bounceAmount, ForceMode2D.Impulse);
//				} else {
//					// Bounce to the right
//					Debug.Log ("We touched the right side of the enemy!");
//					v.y = bounceAmount;
//					rb.velocity = v;
//					rb.AddForce (Vector2.right * -bounceAmount, ForceMode2D.Impulse);
//				}
//
//
//
//			}
//		
//		} 
//
//		Debug.Log (coll.gameObject.name);
//	}

	public void DamageEnemy (int damage){
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
			//GameMasterCS.KillEnemy(this);
			return;
		}

		anim.SetBool ("IsHurt", true);

	//	Debug.Log ("His starting health was" + startHealth);
	//	Debug.Log ("His current health is" + stats.Health);
		
		float damagePercent = (float)stats.Health / startHealth;
	//	Debug.Log (damagePercent);
		eh.UpdateHealthBar(damagePercent);
	}

	IEnumerator Wait(float time){

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0) ;
		yield return new WaitForSeconds (2f);
		timer = 0;
	}

	IEnumerator WaitHurt(){

		yield return new WaitForSeconds (1);
		isHurt = false;
		anim.SetBool ("IsHurt", false);
	}

}
