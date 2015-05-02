using UnityEngine;
using System.Collections;

public class Guy : MonoBehaviour {
	
	
	public float maxSpeed = 10f;
	bool facingRight = true;
	public float jumpForce = 2000f;

	
	bool grounded = false;
	bool doubleJump = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		
		if (grounded)
			doubleJump = true;
		//anim.setBool("Grounded", grounded);
		//anim.setFloat("vSpeed", getComponent<Rigidbody2D>().velocity.y)
		
		float move = Input.GetAxis("Horizontal");

		GetComponent<Rigidbody2D>().velocity = new Vector2(move*maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip();		
		}
	}
	
	void Update() {
		
		//Ekki gott getKeyDown
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			//anim.setBool("Grounded", false);
			grounded = false;
			
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			
		}
		else if(doubleJump && Input.GetKeyDown (KeyCode.Space)){
			doubleJump = false;
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}
	
	void Flip () {
		
		facingRight = !facingRight;
		
		//Possibly skip theScale
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
