using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	Animator anim;

	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		float move = Input.GetAxis ("Horizontal");
		
		anim.SetFloat ("Speed", Mathf.Abs (move));
		
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		
	}
}
