using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class StartMaster : MonoBehaviour {

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1400f;
	public bool allowAttack = true;
	public bool doubleJump = false;
	Animator anim;
	bool facingRight = true;
	bool hasDoubleJump = false;
	public float maxSpeed = 16f;
	
	// Use this for initialization
	void Awake () {
		// Original settings of player
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData();
		data.heal = 3;
		data.jf = 1410;
		data.hdj = false;
		data.ms = 16f;
		data.mx = 3;
//		data.swordSizeX = 1.3f;
//		data.swordSizeY = 1.3f;
		bf.Serialize(file, data);
		file.Close ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		
		if(grounded){
			doubleJump = false;
		}
		
		
		// Left and right movement
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		if (move > 0 && !facingRight)
			Flip ();
		// If player is moving(in a negative axis) and not facing left then we flip
		else if (move < 0 && facingRight)
			Flip ();

		if (Input.GetButtonDown ("Fire1") && allowAttack) {
			
			anim.SetBool("isKnight3Attacking", true);
			allowAttack = false;
			StartCoroutine (waitAttack ());
			StartCoroutine(stopSpamAttack());
		}
		
		// If player is on ground and space(jump) is pushed then we can jump
		if (grounded && Input.GetButtonDown ("Vertical")) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		} else if(!doubleJump && Input.GetButtonDown ("Vertical")){
			if(hasDoubleJump){
				doubleJump = true;
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			}
		}
		
	}

	void Flip(){
		
		// Change the direction and flip the world
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator stopSpamAttack(){
		yield return new WaitForSeconds (0.5f);
		allowAttack = true;
		
	}

	IEnumerator waitAttack(){
		
		//		if (anim.GetFloat ("Speed") > 0) {
		//			yield return new WaitForSeconds (.19f);
		//		} else {
		yield return new WaitForSeconds (0.34f);
		//		}
		
		anim.SetBool("isKnight3Attacking", false);
	}
	
}
