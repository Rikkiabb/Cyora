using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelTwoMaster : MonoBehaviour {

	bool windLeft, windRight, windOff;
	int placement;

	void Start(){
		windLeft = false;
		windRight = false;
		windOff = false;
		placement = 0;
	}

	void OnTriggerEnter2D (Collider2D obj){
		if (obj.name == "Player") {
		
			if(!windLeft && placement == 0){
				windLeft = true;
				StartCoroutine (waitEffectFirstCloud ());
				Physics2D.gravity = new Vector2(-115, -30);
			}

			if(!windRight && placement == 1){
				windRight = true;
				StartCoroutine (waitEffectSecondCloud ());
				Physics2D.gravity = new Vector2(115, -30);
			}

			if(!windOff && placement == 2){
				windRight = true;
				GameObject button = GameObject.FindGameObjectWithTag("Button");
				Animator shrink = button.GetComponent<Animator>();
				shrink.SetTrigger ("Pushed");
				Physics2D.gravity = new Vector2(0, -30);

				GameObject[] arr = GameObject.FindGameObjectsWithTag("Door");
				for(int i = 0; i < arr.Length; i++){
					Animator fade = arr[i].GetComponent<Animator>();
					fade.SetTrigger("Fade");
				}

				GameObject[] arr1 = GameObject.FindGameObjectsWithTag("Enemy");
				for(int i = 0; i < arr1.Length; i++){
					Animator fade = arr1[i].GetComponent<Animator>();
					fade.SetTrigger("Fade");
				}

				StartCoroutine (waitFade ());
//				GameObject[] arr = GameObject.FindGameObjectsWithTag("Door");
//				for(int i = 0; i < arr.Length; i++){
//					StartCoroutine (waitFade ());
//					Animator fade = arr[i].GetComponent<Animator>();
//					fade.SetTrigger("Fade");
//					Destroy (arr[i]);
//				}
			}
		
		}
	}

	IEnumerator waitFade(){
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Door");
		GameObject[] arr1 = GameObject.FindGameObjectsWithTag("Enemy");

		yield return new WaitForSeconds (2);

		for(int i = 0; i < arr.Length; i++){
			Destroy (arr[i]);
		}

		for(int i = 0; i < arr1.Length; i++){
			Destroy (arr1[i]);
		}
		placement++; 
	}



	IEnumerator waitEffectFirstCloud(){
		
		yield return new WaitForSeconds (1);
		GameObject[] arr0 = GameObject.FindGameObjectsWithTag("WindCloud");
		for(int i = 0; i < arr0.Length; i++){
			arr0[i].AddComponent<Rigidbody2D> ();
		}
		GameObject fly3 = GameObject.FindGameObjectWithTag("MoveWindCloud");
		Rigidbody2D rb = fly3.GetComponent<Rigidbody2D> ();
		rb.isKinematic = false;
		rb.mass = 6;
		rb.gravityScale = 25;
		placement++;
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Stair");
		for(int i = 0; i < arr.Length; i++){
			arr[i].AddComponent<Rigidbody2D> ();
			Rigidbody2D rb1 = arr[i].GetComponent<Rigidbody2D> ();
			rb1.mass = 0;
			rb1.gravityScale = 15;
		}
		
	}

	IEnumerator waitEffectSecondCloud(){
		
		yield return new WaitForSeconds (1);
		GameObject fly = GameObject.FindGameObjectWithTag("SecondMoveWind");
		Rigidbody2D rb = fly.GetComponent<Rigidbody2D> ();
		rb.isKinematic = false;
		rb.mass = 6;
		rb.gravityScale = 25;
		GameObject fly1 = GameObject.FindGameObjectWithTag("SecondWind");
		fly1.AddComponent<Rigidbody2D> ();
//		Rigidbody2D rb = fly1.GetComponent<Rigidbody2D> ();
//		rb.mass = 3;
//		rb.gravityScale = 50;
		placement++;
		
	}

	void Update(){
		if (CanvasController.clearedLevel) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
			PlayerData data = new PlayerData();

			if(ScoreManager.numbKeys == 0){ // Nothing happens

				// Setting variables, could use a constructor for a smaller code

				data.heal = 3;
				data.jf = 1410;
				data.hdj = false;
				data.swordSizeX = 1.3f;
				data.swordSizeY = 1.3f;

			} else if(ScoreManager.numbKeys == 1 || ScoreManager.numbKeys == 2){ // Double jump 

				data.heal = 3;
				data.jf = 1410;
				data.hdj = true;
				data.swordSizeX = 1.3f;
				data.swordSizeY = 1.3f;

			}  else if(ScoreManager.numbKeys == 3 || ScoreManager.numbKeys == 4){ // extra life

				data.heal = 4;
				data.jf = 1410;
				data.hdj = false;
				data.swordSizeX = 1.3f;
				data.swordSizeY = 1.3f;
				
			}  else if(ScoreManager.numbKeys == 5){ // bigger sword

				data.heal = 3;
				data.jf = 1410;
				data.hdj = false;
				data.swordSizeX = 1.6f;
				data.swordSizeY = 1.6f;
				
			}
			bf.Serialize(file, data);
			file.Close ();
			CanvasController.clearedLevel = false;
			Application.LoadLevel("Icy");
		}
	}
	/* 		PLAYER TO NORMAL STATE	
			PlayerScript playa = target.gameObject.GetComponent<PlayerScript>();
			playa.setHealth(3);
			playa.setJumpForce(1410);
			playa.hasDoubleJump = false;


			GameObject sword = GameObject.FindGameObjectWithTag("Sword");
			sword.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
	 */

}
