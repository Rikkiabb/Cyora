using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public Transform keyEffect;

	//the source file for the audio effect
//	public AudioClip keyCollect;
//	private AudioSource source;

	//To trigger dissapearing
	public bool dissapear = false;
//	void Start(){
//		//source = GetComponent<AudioSource>();
//	}
//
	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){


			Instantiate(keyEffect, transform.position, transform.rotation);
			ScoreManager.numbKeys++;

			//source.PlayOneShot(keyCollect, 1f);


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
