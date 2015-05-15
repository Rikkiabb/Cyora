using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	EnemyHealth eh;
	Animator anim;
	public bool isHurt = false;
	private AudioSource	source;
	public AudioClip enemyDeath;
	private int startHealth;
	void Start(){
		// Get a reference to the enemy health script
		eh = gameObject.GetComponentInChildren<EnemyHealth>();
		// Get the initial health of the enemy
		startHealth = stats.Health;
		anim = GetComponent<Animator> ();
		StartCoroutine (WaitForward (movement.maxTimer));
		source = transform.parent.GetComponent<AudioSource>();
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
		public float maxTimer = 10f;
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
		
	}
	
	//This thing times yhe movement of your enemy so it moves corectly back and fourth
	IEnumerator WaitForward(float waitTime){
		
		yield return new WaitForSeconds (waitTime);
		movement.forward = !movement.forward;
		StartCoroutine (WaitForward (waitTime));
	}
	
	// How long before he can occur damage again
	public float repeatDamagePeriod = 2f;	
	// The last time he was hit
	private float lastHitTime;
	
	public void DamageEnemy (int damage){
		if (isHurt) {
			return;
		}
		isHurt = true;
		StartCoroutine (WaitHurt ());
		stats.Health -= damage;
		
		// So if our player empties his health he dies
		if(stats.Health <= 0){
//			Debug.Log("Kill Enemy!!");
			transform.gameObject.SetActive(false);
			source.clip = enemyDeath;
			source.Play();
			//			Destroy (transform.parent.gameObject);
			return;
		}
		
		anim.SetBool ("IsHurt", true);
		
		float damagePercent = (float)stats.Health / startHealth;
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
