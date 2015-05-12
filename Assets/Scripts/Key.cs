using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public Transform keyEffect;

	//the source file for the audio effect
	public AudioClip keyCollect;
	private AudioSource source;

	//To trigger dissapearing
	public bool dissapear = false;
	

	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){


			Instantiate(keyEffect, transform.position, transform.rotation);
			ScoreManager.numbKeys++;
			source = GetComponent<AudioSource>();
			// play the sound effect
			source.PlayOneShot(keyCollect, 1F);		
			GameObject effect2 = GameObject.FindGameObjectWithTag("KeyEffect");
			Destroy(effect2, 2);
//			Destroy(gameObject);
			gameObject.SetActive(false);
		

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
