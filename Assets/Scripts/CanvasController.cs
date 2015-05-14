using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class CanvasController : MonoBehaviour {

	public Transform target;
	public static Animator anim;
	bool hasExited = false;
	static public bool clearedLevel;
	public Transform aKey;
	public Transform aHeart;
	
	void Awake(){
		clearedLevel = false;
	}
	void Start () {
		anim = GetComponent<Animator> ();
		clearedLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript player = target.gameObject.GetComponent<PlayerScript> ();
		if((player.playerStats.Health < 1) && !hasExited){
			// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
			anim.SetTrigger("GameOver");
			hasExited = true;
//			Physics2D.gravity = new Vector2(0, -30);

		}

		if (player.playerStats.Health > 0 && clearedLevel) {
			// Þurfum að búa til scene hérna
			Physics2D.gravity = new Vector2(0, -30);

		}

		if (hasExited) {
			if(Input.GetButtonDown ("Mouse X")){ // R
				anim.SetTrigger ("Restart");
				hasExited = false;
				if(LevelTwoMaster.windRight){
					Physics2D.gravity = new Vector2(115, -30);
				} else if (LevelTwoMaster.windLeft){
					Physics2D.gravity = new Vector2(-115, -30);
				} else {
					Physics2D.gravity = new Vector2(0, -30);
				}
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
//				ScoreManager.numbKeys = 0;
//				Application.LoadLevel(Application.loadedLevel);
			}
			if(Input.GetButtonDown ("Fire2")){ // M
				PlayerScript.isMoving = true;
				ScoreManager.numbKeys = 0;
				Application.LoadLevel("MainMenu");
			}
			if(Input.GetButtonDown ("Fire3")){ // Q
				Application.Quit();
			}
		}
	
	}


	
}
