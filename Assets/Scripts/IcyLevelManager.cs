﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class IcyLevelManager : MonoBehaviour {

	public Transform target;
	PlayerScript player;
	public Transform aKey;
	public Transform aHeart;
	bool first, second, skip;


	// Use this for initialization

	void Start(){
		first = second = skip = false;
		GameMasterCS.setIce(true);
		// save player stats and level stats
		player = target.gameObject.GetComponent<PlayerScript> ();
		player.fallBoundary = 700	;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/checkpoint.dat");
		CheckpointReached data = new CheckpointReached();
		
		data.playPosX = target.position.x;
		data.playPosY = target.position.y;
		data.playPosZ = target.position.z;
		data.health = player.playerStats.Health;
		data.currKeys = 0;
		
		GameObject[] locKeys = GameObject.FindGameObjectsWithTag("Key");
		for (int i = 0; i < locKeys.Length; i++) {
			data.keysX.Add(locKeys[i].transform.position.x);
			data.keysY.Add(locKeys[i].transform.position.y);
			data.keysZ.Add(locKeys[i].transform.position.z);
		}
		
		GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
		for (int i = 0; i < hearts.Length; i++) {
			data.heartX.Add(hearts[i].transform.position.x);
			data.heartY.Add(hearts[i].transform.position.y);
			data.heartZ.Add(hearts[i].transform.position.z);
		}
		
		bf.Serialize(file, data);
		file.Close ();
	}
	// Update is called once per frame
	void Update () {
		
		if (CanvasController.clearedLevel) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
			PlayerData data = new PlayerData();
			
			if(ScoreManager.numbKeys == 0 || ScoreManager.numbKeys == 1){ // Nothing happens
				
				// Setting variables, could use a constructor for a smaller code
				
				data.heal = 3;
				data.jf = 1410;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("NoReward");
				
			} else if(ScoreManager.numbKeys == 2 || ScoreManager.numbKeys == 3){ // Extra life
				
				data.heal = 4;
				data.jf = 1410;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 4;

				CanvasController.anim.SetTrigger("ExtraLife");
				
			}  else if(ScoreManager.numbKeys == 4){ // extra jump
				
				data.heal = 3;
				data.jf = 1800;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("ExtraJump");
				
			}  else if(ScoreManager.numbKeys == 5){ // extra speed
				
				data.heal = 3;
				data.jf = 1410;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 20f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("ExtraSpeed");
				
			} else if(ScoreManager.numbKeys == 6){ // double jump
				
				data.heal = 3;
				data.jf = 1410;
				data.hdj = true;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("DoubleJump");
				
			}
			bf.Serialize(file, data);
			file.Close ();
			CanvasController.clearedLevel = false;
			PlayerScript.finished = false;
			skip = true;

			StartCoroutine(waitToShowUpgrade());

		}

		if (skip) {
			if (Input.GetButtonDown ("Fire1")) {
				Application.LoadLevel("IcyOutro");
			}
		}
		
	}

	IEnumerator waitToShowUpgrade(){
		yield return new WaitForSeconds (14);
		Application.LoadLevel("IcyOutro");
	}
	
	//
	
	
	void OnTriggerEnter2D (Collider2D obj){
		
		if (obj.name == "Player" && PlayerScript.isMoving) {

			if(!first){
				first = true;
				GameObject firstCheck = GameObject.FindGameObjectWithTag("CheckLeft");
				BoxCollider2D bc = firstCheck.GetComponent<BoxCollider2D> ();
				bc.enabled = false;

			} else if (first && !second){
				second = true;
				GameObject secondCheck = GameObject.FindGameObjectWithTag("CheckRight");
				BoxCollider2D bc = secondCheck.GetComponent<BoxCollider2D> ();
				bc.enabled = false;
			}

			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/checkpoint.dat", FileMode.Open);
			CheckpointReached data = new CheckpointReached();
			
			data.playPosX = target.position.x;
			data.playPosY = (target.position.y + 5);
			data.playPosZ = target.position.z;
			data.health = player.playerStats.Health;
			data.currKeys = ScoreManager.numbKeys;
			GameObject[] locKeys = GameObject.FindGameObjectsWithTag("Key");
			for (int i = 0; i < locKeys.Length; i++) {
				data.keysX.Add(locKeys[i].transform.position.x);
				data.keysY.Add(locKeys[i].transform.position.y);
				data.keysZ.Add(locKeys[i].transform.position.z);
			}
			
			GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
			for (int i = 0; i < hearts.Length; i++) {
				data.heartX.Add(hearts[i].transform.position.x);
				data.heartY.Add(hearts[i].transform.position.y);
				data.heartZ.Add(hearts[i].transform.position.z);
			}
			
			bf.Serialize(file, data);
			file.Close ();
		}
		
	}
}
