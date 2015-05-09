using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int num = 3;
	private SpriteRenderer healthBar;
	
	private Vector3 healthScale;
	// Use this for initialization
	void Awake () {
	
		healthBar = transform.gameObject.GetComponent<SpriteRenderer>();
		//healthBar.enabled = false;
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void UpdateHealthBar (float healthPercentage)
	{
		
		// Set the scale of the health bar to be proportional to the player's health.

		healthBar.transform.localScale = new Vector3(healthScale.x * healthPercentage, 1, 1);
	}
}
