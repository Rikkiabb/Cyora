using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
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
	}

	//For enemy movement.
	int timer = 0;

	// instantiate
	public EnemyStats stats = new EnemyStats();
	public EnemyMovement movement = new EnemyMovement();
	// Deal a damage to this player
	public int fallBoundary = -5;

	// how much a player will bounce of enemy on contact
	public float bounceAmount = 15f;
	// this update isn't really neccessary for our enemy class but could be useful later on TODO refactor
	void Update(){
		if (transform.position.y <= fallBoundary){
			//This shouldn't be a issue because enemies don't fall down
			Debug.Log ("Enemy fell to his death");
			DamageEnemy(9999);
		}

		Move ();
	}

	void Move(){

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
			timer = 0;
			movement.forward = !movement.forward;
		}

	}

	// How long before he can occur damage again
	public float repeatDamagePeriod = 2f;	
	// The last time he was hit
	private float lastHitTime;
	// This function gets called whenever something collides with our thingy
	void OnCollisionEnter2D(Collision2D coll){

		if (coll.gameObject.tag == "Player" && coll.collider.tag != "Sword") {

			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// We need to get the incoming collider that was involved in the collision
				//TODO: Remove these stupid logs when we build the project
				// We want to check the vector perpendicular to the surface of the incoming Collider2D at the contact point.
				Vector2 pointOfContact = coll.contacts [0].normal; //Grab the normal of the contact point we touched
				Debug.Log (pointOfContact);
				// Store an instance of the player thad collided with the enemy

				PlayerScript player = coll.gameObject.GetComponent<PlayerScript> ();
				// Deal appropriate damage to him 
				player.DamagePlayer (stats.attackHit);
				lastHitTime = Time.time;

				// Get the rigidbody of our player so we can manipulate it
				Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();
				
				// What we need to to is find out which side of the collider hit our player and apply force to him
				// in the opposite direction
				Vector2 v = rb.velocity;
			
				//Detect which side of the collider we touched
				// So what the normal does is gives us a two dimensional representation of the direction which the player 
				// is approaching from
				// A vector(-1,0) means we are coming from the right side when we impact
				if (player.facingRight) {
					// Bounce to the left
					Debug.Log ("We touched the left side of the enemy!");
					v.y = bounceAmount;
					rb.velocity = v;
					rb.AddForce (Vector2.right * bounceAmount, ForceMode2D.Impulse);
				} else {
					// Bounce to the right
					Debug.Log ("We touched the right side of the enemy!");
					v.y = bounceAmount;
					rb.velocity = v;
					rb.AddForce (Vector2.right * -bounceAmount, ForceMode2D.Impulse);
				}



			}
		
		} 

		Debug.Log (coll.gameObject.name);
	}

	public void DamageEnemy (int damage){
		stats.Health -= damage;
		// So if our player empties his health he dies
		if(stats.Health <= 0){
			Debug.Log("Kill Enemy!!");
			Destroy (transform.parent.gameObject);
			//GameMasterCS.KillEnemy(this);
			
		}
	}
}
