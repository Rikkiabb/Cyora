﻿using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {

	public Transform heartEffect;

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
				Destroy(gameObject);
				
			}
		}
	}

	public static void DrawHeart(int hearts){
		Animator animHeart;
		for (int i = 0; i < hearts; i++) {
			string image = "Life" + i;
			GameObject heart = GameObject.FindGameObjectWithTag(image);
			animHeart = heart.GetComponent<Animator> ();
			animHeart.SetTrigger("GainLife");
		}
	}
}
