﻿using UnityEngine;
using System.Collections;

public class HurtColl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// how much a player will bounce of enemy on contact
	public float bounceAmount = 15f;
	// How long before he can occur damage again
	public float repeatDamagePeriod = 2f;	
	// The last time he was hit
	private float lastHitTime;
	// This function gets called whenever something collides with our thingy
	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("HURT HIM!");
		PlayerScript player = coll.gameObject.GetComponent<PlayerScript> ();

		if (coll.gameObject.tag == "Player" && (coll.collider.tag != "Sword" || (coll.collider.tag == "Sword" && !player.isAttacking()))) {

			if (Time.time > lastHitTime + repeatDamagePeriod) {
				// We need to get the incoming collider that was involved in the collision
				//TODO: Remove these stupid logs when we build the project
				// We want to check the vector perpendicular to the surface of the incoming Collider2D at the contact point.
				Vector2 pointOfContact = coll.contacts [0].normal; //Grab the normal of the contact point we touched
				Debug.Log (pointOfContact);
				// Store an instance of the player thad collided with the enemy


				// Deal appropriate damage to him 
				player.DamagePlayer (transform.parent.gameObject.GetComponent<Enemy>().stats.attackHit);
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
}
