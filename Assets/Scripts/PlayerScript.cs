using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerScript : MonoBehaviour {
	
	public AudioClip attackSound;
	public AudioClip keyCollect;
	public AudioClip jumpSound;
	public AudioClip deathSound;
	public AudioClip hurtSound;
	public AudioClip healthGain;
	
	private AudioSource source;
	
	public int maxHealth = 3;
	CircleCollider2D collide;
	Rigidbody2D freefall;
	public bool hasDoubleJump = false;
	bool doubleJump = false;
	
	public static bool finished = false; 
	bool animPlay = false;
	
	public static bool isMoving = true;
	public bool isHurt = false;
	
	// Create a new player stat class which handles his health and weapons
	[System.Serializable]
	public class PlayerStats {
		public int Health = 3;
	}
	
	// instantiate
	public PlayerStats playerStats = new PlayerStats();
	// Deal a damage to this player
	public int fallBoundary = -30;
	
	
	
	// For left and right movement
	public float maxSpeed = 16f;
	Animator anim;
	
	// Players running to right(true) or left(false)
	public bool facingRight = true;
	
	// Jump variables
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1400f;
	public bool allowAttack = true;
	
	void Awake(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = ((PlayerData)bf.Deserialize (file));
			file.Close ();
			setHealth (data.heal);
			setJumpForce (data.jf);
			hasDoubleJump = data.hdj;
			//			GameObject sword = GameObject.FindGameObjectWithTag ("Sword");
			//			sword.transform.localScale = new Vector3(data.swordSizeX, data.swordSizeY, 1f);
			maxHealth = data.mx;
			maxSpeed = data.ms;
		}
		if (maxHealth > 0) {
			Heart.DrawHeart (maxHealth);
		}
		isMoving = true;
		
	}
	
	void Start () {
		Debug.Log ("Borð" + Application.loadedLevel);
		anim = GetComponent<Animator> ();
		collide = GetComponent<CircleCollider2D> ();
		freefall = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource>();
	}
	
	private float lastHitTime;
	public float repeatSwing = 0f;
	
	void Update(){
		
		if (isMoving) {
			if (Input.GetButtonDown ("Fire1") && allowAttack) {
				// play attack sound
				source.volume = 1f;
				source.clip = attackSound;
				source.Play ();
				anim.SetBool("isKnight3Attacking", true);
				allowAttack = false;
				StartCoroutine (waitAttack ());
				StartCoroutine(stopSpamAttack());
			}
			
			// If player is on ground and space(jump) is pushed then we can jump
			if (grounded && Input.GetButtonDown ("Vertical")) {
				source.volume = 0.2f;
				source.clip = jumpSound;
				source.Play ();
				
				anim.SetBool ("Ground", false);
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			} else if(!doubleJump && Input.GetButtonDown ("Vertical")){
				if(hasDoubleJump){
					source.volume = 0.2f;
					source.clip = jumpSound;
					source.Play ();
					doubleJump = true;
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
				}
			}
		}
		
		if (transform.position.y <= fallBoundary){
			Debug.Log ("Player fell to his death");
			DamagePlayer(maxHealth);
		}
		
		if (finished && !animPlay) {
			anim.SetTrigger("Finished");
			animPlay = true; 
		}
		
	}
	
	public void setHealth(int health){
		playerStats.Health = health;
	}
	
	public void setJumpForce(float jump){
		jumpForce = jump;
	}
	
	
	IEnumerator waitAttack(){
		
		//		if (anim.GetFloat ("Speed") > 0) {
		//			yield return new WaitForSeconds (.19f);
		//		} else {
		yield return new WaitForSeconds (0.34f);
		//		}
		
		anim.SetBool("isKnight3Attacking", false);
	}
	
	
	
	public void DamagePlayer (int damage){
		
		if (damage >= maxHealth) {
			
			Debug.Log("Kill Player!!");
			playerStats.Health = 0;
			//GameMasterCS.KillPlayer(this);
			return;
		}
		// Remove his lives
		// play hurtsound
		if(playerStats.Health > 0){
			source.volume = 1f;
			source.clip = hurtSound;
			source.Play();
		}
		
		playerStats.Health -= damage;
		
		
		// Remove damage many hearts from the canvas
		// for loop that runs through the hearts and removes them until damage is done
		for(int i = damage; i > 0 && playerStats.Health >= 0; i--){
			Animator animHeart;
			Debug.Log ("Damage:" + damage);
			string number = (playerStats.Health).ToString();
			string image = "Life" + number;
			Debug.Log (image);
			GameObject heart = GameObject.FindGameObjectWithTag(image);
			animHeart = heart.GetComponent<Animator> ();
			animHeart.SetTrigger("MissLife");
			
		}
		
		if (isMoving) {
			anim.SetBool ("isKnight3Hurting", true);
			StartCoroutine(WaitAnimationHurt());
		}
		// So if our player empties his health he dies
		if(playerStats.Health == 0){
			Debug.Log("Kill Player!!");
			source.clip = deathSound;
			source.Play();
			return;
		}
	}
	
	void FixedUpdate () {
		
		if (isMoving) {
			// Check if we are on the ground
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);
			
			if(grounded){
				doubleJump = false;
			}
			
			
			// Left and right movement
			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));
			// Fixes the friction stuff in icy level
			
			if(GameMasterCS.isIcy()){
				GetComponent<Rigidbody2D>().AddForce(new Vector2(move * maxSpeed, 0));
			}
			else{
				
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}
			
			// If player is moving(in a positive axis) and not facing right then we flip
			if (move > 0 && !facingRight)
				Flip ();
			// If player is moving(in a negative axis) and not facing left then we flip
			else if (move < 0 && facingRight)
				Flip ();
		}
	}
	
	void Flip(){
		
		// Change the direction and flip the world
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public bool isAttacking(){
		
		return anim.GetBool("isKnight3Attacking");
	}
	
	IEnumerator WaitAnimationHurt(){
		yield return new WaitForSeconds(1f);
		anim.SetBool ("isKnight3Hurting", false);
		
	}
	
	IEnumerator stopSpamAttack(){
		yield return new WaitForSeconds (0.5f);
		allowAttack = true;
		
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Key"){
			source.volume = 0.7f;
			source.clip = keyCollect;
			source.Play();	
		}
		if(coll.tag == "Heart"){
			Debug.Log ("healtsound played");
			source.volume = 1f;
			source.clip = healthGain;
			source.Play();

		}
	}
	
	
}

[System.Serializable]
class PlayerData{
	public int heal;
	public float jf;
	public bool hdj;
	//	public float swordSizeX;
	//	public float swordSizeY;
	public int mx;
	public float ms;
}

[System.Serializable]
class CheckpointReached{
	public float playPosX;
	public float playPosY;
	public float playPosZ;
	public int health;
	public int currKeys;
	public List<float> keysX = new List<float> ();
	public List<float> keysY = new List<float> ();
	public List<float> keysZ = new List<float> ();
	public List<float> heartX = new List<float> ();
	public List<float> heartY = new List<float> ();
	public List<float> heartZ = new List<float> ();
}

