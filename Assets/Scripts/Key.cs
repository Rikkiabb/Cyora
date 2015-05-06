using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public Transform keyEffect;

	//To trigger dissapearing
	public bool dissapear = false;
	
	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){

			GameMasterCS.numbKeys++;

			Instantiate(keyEffect, transform.position, transform.rotation);
			GameObject effect2 = GameObject.FindGameObjectWithTag("KeyEffect");
			Destroy(effect2, 2);
			Destroy(gameObject);

			if(dissapear){
				GameObject explode = GameObject.FindGameObjectWithTag("Explode");	
				GameObject enemy = GameObject.FindGameObjectWithTag("Still");
				Destroy(explode);
				enemy.tag = "Enemy";
				Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
				enemyScript.enabled = true;

			}
		}
	}
}
