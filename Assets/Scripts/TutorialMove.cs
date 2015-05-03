using UnityEngine;
using System.Collections;

public class TutorialMove : MonoBehaviour {

	// For left and right movement
	public float maxSpeed = 10f;
	Animator anim;
	
	// Players running to right(true) or left(false)
	bool facingRight = true;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {

		
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
