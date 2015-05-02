using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// For left and right movement
	public float maxSpeed = 10f;
	Animator anim;

	// Players running to right(true) or left(false)
	bool facingRight = true;

	// Jump variables
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	void Start () {
		anim = GetComponent<Animator> ();
		
	}

	void Update(){

		// If player is on ground and space(jump) is pushed then we can jump
		if (grounded && Input.GetButtonDown ("Jump")) {

			anim.SetBool("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
	}

	void FixedUpdate () {

		// Check if we are on the ground
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		// Left and right movement
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
		// If player is moving(in a positive axis) and not facing right then we flip
		if (move > 0 && !facingRight)
			Flip ();
		// If player is moving(in a negative axis) and not facing left then we flip
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Flip(){

		// Change the direction and flip the world
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
