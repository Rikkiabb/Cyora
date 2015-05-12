using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelOneMaster : MonoBehaviour {
	
	public Transform target;
	PlayerScript player;
	public Transform aKey;
	public Transform aHeart;
	
	void Start(){

		// save player stats and level stats
		player = target.gameObject.GetComponent<PlayerScript> ();
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

	void Update(){
		if (player.playerStats.Health < 1) {
			StartCoroutine(waitToSpawn());
		}

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
				
			} else if(ScoreManager.numbKeys == 2 || ScoreManager.numbKeys == 3){ // Extra life
				
				data.heal = 4;
				data.jf = 1410;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 4;

				
			}  else if(ScoreManager.numbKeys == 4){ // extra jump
				
				data.heal = 3;
				data.jf = 1800;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;
				
			}  else if(ScoreManager.numbKeys == 5){ // extra speed
				
				data.heal = 3;
				data.jf = 1410;
				data.hdj = false;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 20f;
				data.mx = 3;
				
			} else if(ScoreManager.numbKeys == 6){ // double jump
				
				data.heal = 3;
				data.jf = 1410;
				data.hdj = true;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;
				
			}
			bf.Serialize(file, data);
			file.Close ();
			CanvasController.clearedLevel = false;
			Application.LoadLevel("Two");
		}
	
	}
	
//
	IEnumerator waitToSpawn(){
		yield return new WaitForSeconds (1);
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/checkpoint.dat", FileMode.Open);
			CheckpointReached data = ((CheckpointReached)bf.Deserialize (file));
			file.Close ();
			Vector3 temp = new Vector3(data.playPosX, data.playPosY, data.playPosZ);
			player.transform.position = temp;
			player.playerStats.Health = data.health;
			Heart.DrawHeart (data.health);
			ScoreManager.numbKeys = data.currKeys;

			GameObject[] center = GameObject.FindGameObjectsWithTag("KeyCenter");
			for (int i = 0; i < center.Length; i++) {
				Destroy(center[i]);
			}
			GameObject[] remKey = GameObject.FindGameObjectsWithTag("Key");
			for (int i = 0; i < remKey.Length; i++) {
				Destroy(remKey[i]);
			}
			Quaternion temp1 = new Quaternion(0f, 0f, 0f, 0f);
			float[] kx = data.keysX.ToArray();
			float[] ky = data.keysY.ToArray();
			float[] kz = data.keysZ.ToArray();
			for(int i = 0; i < kx.Length; i++){
				Vector3 temp0 = new Vector3(kx[i], ky[i], kz[i]);
				Instantiate(aKey, temp0, temp1);
			}

			GameObject[] destHeart = GameObject.FindGameObjectsWithTag("Heart");
			for (int i = 0; i < destHeart.Length; i++) {
				Destroy(destHeart[i]);
			}
			float[] hx = data.heartX.ToArray();
			float[] hy = data.heartY.ToArray();
			float[] hz = data.heartZ.ToArray();
			for(int i = 0; i < kx.Length; i++){
				Vector3 temp0 = new Vector3(hx[i], hy[i], hz[i]);
				Instantiate(aHeart, temp0, temp1);
			}
			
		}

	}
	

	void OnTriggerEnter2D (Collider2D obj){
		
		if (obj.name == "Player") {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/checkpoint.dat", FileMode.Open);
			CheckpointReached data = new CheckpointReached();
			
			data.playPosX = target.position.x;
			data.playPosY = target.position.y;
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
