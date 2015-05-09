using UnityEngine;
using System.Collections;

public class LevelOneMaster : MonoBehaviour {

	Vector3 lastCheckPoint;
	public Transform target;
	PlayerScript player;
	int checkHealth;
	int currKeys;
	GameObject[] locKeys;

	void Awake(){
		player = target.gameObject.GetComponent<PlayerScript> ();
		lastCheckPoint = new Vector3 (-34.8f, -2.3f, 0f);
		checkHealth = player.playerStats.Health;
		currKeys = 0;
		locKeys = GameObject.FindGameObjectsWithTag("Key");
	}

	void Update(){
		if (player.playerStats.Health < 1) {
			// gera courentine til að hafa smá mismun
			StartCoroutine(waitToSpawn());
			Debug.Log("Check"+lastCheckPoint);
		}
	}

	IEnumerator waitToSpawn(){
		yield return new WaitForSeconds (1);
		player.transform.position = lastCheckPoint;
		player.setMove (true);
		player.playerStats.Health = checkHealth;
		Heart.DrawHeart (checkHealth - 1);
		ScoreManager.numbKeys = currKeys;
//		GameObject remKey = GameObject.FindGameObjectsWithTag("Key");



	}

	void OnTriggerEnter2D (Collider2D obj){
		
		if (obj.name == "Player") {
			lastCheckPoint = obj.transform.position;
			Debug.Log ("Collide:" + obj.transform.position);
			checkHealth = player.playerStats.Health;
			currKeys = ScoreManager.numbKeys;
			locKeys = GameObject.FindGameObjectsWithTag("Key");
		}

	}

}
