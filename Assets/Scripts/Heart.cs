using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {
	//private AudioSource	source;
	//public AudioClip extraLife;
	public Transform heartEffect;

//	void Start(){
//		source = transform.parent.GetComponent<AudioSource>();
//	}

	void OnTriggerEnter2D (Collider2D obj){
		
		if (obj.name == "Player") {
			PlayerScript player = obj.gameObject.GetComponent<PlayerScript>();

			if(player.maxHealth > player.playerStats.Health){
				Animator animHeart;
				string number = (player.playerStats.Health).ToString();
				string image = "Life" + number;
				GameObject heart = GameObject.FindGameObjectWithTag(image);
				animHeart = heart.GetComponent<Animator> ();
				animHeart.SetTrigger("GainLife");

				Instantiate(heartEffect, transform.position, transform.rotation);
				player.setHealth(player.playerStats.Health + 1);
				// Instead of destroying, we disable it. 
//				source.clip = extraLife;
//				source.Play();
//				transform.gameObject.SetActive(false);
				Destroy(gameObject);
				
			}
		}
	}


	public static void DrawHeart(int hearts){
		Debug.Log(Application.loadedLevel);
		if(Application.loadedLevel != 14 || Application.loadedLevel != 15 || Application.loadedLevel != 16 || Application.loadedLevel != 17
		   || Application.loadedLevel != 18 || Application.loadedLevel != 19){
			Animator animHeart;
			for (int i = 0; i < hearts; i++) {
				string image = "Life" + i;
				GameObject heart = GameObject.FindGameObjectWithTag(image);
				animHeart = heart.GetComponent<Animator> ();
				animHeart.SetTrigger("GainLife");
			}
		}
	}
}
