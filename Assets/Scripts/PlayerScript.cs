using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int maxHealth = 3;
	CircleCollider2D collide;
	Rigidbody2D freefall;
		
	public static bool finished = false; 
	bool animPlay = false;

	public static bool isMoving = true;

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
	public float maxSpeed = 10f;
	Animator anim;

	// Players running to right(true) or left(false)
	public bool facingRight = true;

	// Jump variables
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1100f;

	void Start () {
		anim = GetComponent<Animator> ();
		collide = GetComponent<CircleCollider2D> ();
		freefall = GetComponent<Rigidbody2D> ();
	}

	private float lastHitTime;
	public float repeatSwing = 0f;

	void Update(){

		if (isMoving) {
			if (Input.GetButtonDown ("Fire1") && !anim.GetBool ("isKnight3Attacking")) {

				anim.SetBool("isKnight3Attacking", true);
				StartCoroutine (waitAttack ());
			}
		
			// If player is on ground and space(jump) is pushed then we can jump
			if (grounded && Input.GetButtonDown ("Jump")) {

				anim.SetBool ("Ground", false);
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
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

	IEnumerator waitAttack(){

		if (anim.GetFloat ("Speed") > 0) {
			yield return new WaitForSeconds (.8f);
		} else {
			yield return new WaitForSeconds (0.6f);
		}

		anim.SetBool("isKnight3Attacking", false);
	}


	
	public void DamagePlayer (int damage){

		if (damage >= maxHealth) {
			GameObject heart1 = GameObject.FindGameObjectWithTag("Life0");
			GameObject heart2 = GameObject.FindGameObjectWithTag("Life1");
			GameObject heart3 = GameObject.FindGameObjectWithTag("Life2");

			Destroy(heart1);
			Destroy(heart2);
			Destroy(heart3);

			Debug.Log("Kill Player!!");
			GameMasterCS.KillPlayer(this);

			return;
		}
		// Remove one of his lives
		playerStats.Health -= damage;
	

		// Remove damage many hearts from the canvas
		// for loop that runs through the hearts and removes them until damage is done
		for(int i = damage; i > 0;i--){
			Debug.Log ("Damage:" + damage);
			string number = (playerStats.Health).ToString();
			string image = "Life" + number;
			Debug.Log (image);
			GameObject heart = GameObject.FindGameObjectWithTag(image);
			Destroy(heart);

		}

		if (isMoving) {
			anim.SetBool ("isKnight3Hurting", true);
			StartCoroutine(WaitHurt());
		}
		// So if our player empties his health he dies
		if(playerStats.Health <= 0){
			Debug.Log("Kill Player!!");
			freefall.mass = 2000;
			Destroy (collide);
			GameObject Sword = GameObject.FindGameObjectWithTag("Sword");
			Destroy(Sword);
			isMoving = false;
		}
	}

	void FixedUpdate () {

		if (isMoving) {
			// Check if we are on the ground
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);

			// Left and right movement
			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			
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

	IEnumerator WaitHurt(){
		yield return new WaitForSeconds(1f);
		anim.SetBool ("isKnight3Hurting", false);

	}

	
}
