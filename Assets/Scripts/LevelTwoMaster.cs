using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelTwoMaster : MonoBehaviour {

	static public bool windLeft, windRight, windOff;
	public Transform target;
	PlayerScript player;
	public Transform aKey;
	AudioSource source;
	public Transform aHeart;

	void Start(){
		player = target.gameObject.GetComponent<PlayerScript> ();
		windLeft = false;
		windRight = false;
		windOff = false;
		source = GetComponent<AudioSource>();
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

	void OnTriggerEnter2D (Collider2D obj){
		if (obj.name == "Player" && PlayerScript.isMoving) {


			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/checkpoint.dat", FileMode.Open);
			CheckpointReached data = new CheckpointReached();

			data.playPosX = (target.position.x + 18f);
			data.playPosY = (target.position.y);
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

	
			if(!windLeft){

				StartCoroutine (waitEffectFirstCloud ());
				Physics2D.gravity = new Vector2(-115, -30);
			} else if(!windRight){

				StartCoroutine (waitEffectSecondCloud ());
				Physics2D.gravity = new Vector2(115, -30);
			} else if(!windOff){

				GameObject button = GameObject.FindGameObjectWithTag("Button");
				Animator shrink = button.GetComponent<Animator>();
				shrink.SetTrigger ("Pushed");
				source.Play();
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
			}
		}
		

	}

	IEnumerator waitFade(){
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Door");
		GameObject[] arr1 = GameObject.FindGameObjectsWithTag("Enemy");

		for(int i = 0; i < arr.Length; i++){
			Destroy (arr[i]);
		}
		
		for(int i = 0; i < arr1.Length; i++){
			Destroy (arr1[i]);
		}

		yield return new WaitForSeconds (2);
		windOff = true;
		

	}



	IEnumerator waitEffectFirstCloud(){
		GameObject[] arr0 = GameObject.FindGameObjectsWithTag("WindCloud");
		for(int i = 0; i < arr0.Length; i++){
			arr0[i].AddComponent<Rigidbody2D> ();
		}
		GameObject fly3 = GameObject.FindGameObjectWithTag("MoveWindCloud");
		Rigidbody2D rb = fly3.GetComponent<Rigidbody2D> ();
		rb.isKinematic = false;
		rb.mass = 6;
		rb.gravityScale = 25;
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Stair");
		for(int i = 0; i < arr.Length; i++){
			arr[i].AddComponent<Rigidbody2D> ();
			Rigidbody2D rb1 = arr[i].GetComponent<Rigidbody2D> ();
			rb1.mass = 0;
			rb1.gravityScale = 15;
		}
		GameObject windL = GameObject.FindGameObjectWithTag("CheckLeft");
		BoxCollider2D bc = windL.GetComponent<BoxCollider2D> ();
		bc.enabled = false;
		windLeft = true;
		yield return new WaitForSeconds (5);

		
		
	}

	IEnumerator waitEffectSecondCloud(){

		GameObject fly = GameObject.FindGameObjectWithTag("SecondMoveWind");
		Rigidbody2D rb = fly.GetComponent<Rigidbody2D> ();
		rb.isKinematic = false;
		rb.mass = 6;
		rb.gravityScale = 25;
		GameObject fly1 = GameObject.FindGameObjectWithTag("SecondWind");
		fly1.AddComponent<Rigidbody2D> ();
		GameObject windR = GameObject.FindGameObjectWithTag("CheckRight");
		BoxCollider2D bc = windR.GetComponent<BoxCollider2D> ();
		bc.enabled = false;
		windRight = true;
		yield return new WaitForSeconds (5);

//		Rigidbody2D rb = fly1.GetComponent<Rigidbody2D> ();
//		rb.mass = 3;
//		rb.gravityScale = 50;
		
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
//				data.swordSizeX = 1.3f;
//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("NoReward");

			} else if(ScoreManager.numbKeys == 1){ // Extra jump power 

				data.heal = 3;
				data.jf = 1800;
				data.hdj = true;
//				data.swordSizeX = 1.3f;
//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("ExtraJump");

			}  else if(ScoreManager.numbKeys == 2){ // Double jump 
				
				data.heal = 3;
				data.jf = 1410;
				data.hdj = true;
				//				data.swordSizeX = 1.3f;
				//				data.swordSizeY = 1.3f;
				data.ms = 16f;
				data.mx = 3;
				
				CanvasController.anim.SetTrigger("DoubleJump");

			} else if(ScoreManager.numbKeys == 3){ // extra speed

				data.heal = 3;
				data.jf = 1410;
				data.hdj = false;
//				data.swordSizeX = 1.3f;
//				data.swordSizeY = 1.3f;
				data.ms = 20f;
				data.mx = 3;

				CanvasController.anim.SetTrigger("ExtraSpeed");
				
			}  else if(ScoreManager.numbKeys == 4 || ScoreManager.numbKeys == 5){ // extra life

				data.heal = 4;
				data.jf = 1410;
				data.hdj = false;
//				data.swordSizeX = 1.6f;
//				data.swordSizeY = 1.6f;
				data.ms = 16f;
				data.mx = 4;

				CanvasController.anim.SetTrigger("ExtraLife");
				
			}
			bf.Serialize(file, data);
			file.Close ();
			CanvasController.clearedLevel = false;
			windLeft = false;
			windRight = false;

			StartCoroutine(waitToShowUpgrade());
		}
	}

	IEnumerator waitToShowUpgrade(){
		yield return new WaitForSeconds (14);
		Application.LoadLevel("WindyOutro");
	}
	


}
