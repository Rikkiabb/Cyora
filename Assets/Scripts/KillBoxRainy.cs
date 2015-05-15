using UnityEngine;
using System.Collections;

public class KillBoxRainy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Move the killbox up
		if (PlayerScript.finished) {
			StopEnemiesAndCamera();

		} else {
			if(RainyStart.started){
				Vector3 temp = transform.position;
				temp.y += 0.05f;
				transform.position = temp;
			}

		}

	}

	void OnTriggerEnter2D(Collider2D obj){


		if (obj.name == "Player") {
			PlayerScript player = obj.gameObject.GetComponent<PlayerScript>();
			//Kill the player
			player.DamagePlayer(player.maxHealth);
			StopEnemiesAndCamera();

		}
	}

	void StopEnemiesAndCamera(){

		//Stop camera and water
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		GameObject water = GameObject.FindGameObjectWithTag("Water");
		CameraRainy cr = camera.gameObject.GetComponent<CameraRainy>();
		cr.enabled = false;
		cr = water.gameObject.GetComponent<CameraRainy>();
		cr.enabled = false;

		//Stop every enemy at the bottom.
		GameObject[] arr = GameObject.FindGameObjectsWithTag("WaterEnemy");

		for(int i = 0; i < arr.Length; i++){

			cr = arr[i].gameObject.GetComponent<CameraRainy>();
			cr.enabled = false;
		}
	}
	
}
