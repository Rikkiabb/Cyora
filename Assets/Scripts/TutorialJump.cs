using UnityEngine;
using System.Collections;

public class TutorialJump : MonoBehaviour {

	Animator anim;

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

	}
}
