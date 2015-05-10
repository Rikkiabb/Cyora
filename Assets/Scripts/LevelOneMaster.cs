using UnityEngine;
using System.Collections;

public class LevelOneMaster : MonoBehaviour {

	Vector3 lastCheckPoint;
	public Transform target;
	PlayerScript player;
	int checkHealth;
//	int currKeys;
//	GameObject[] locKeys;
	public Transform aKey;


	void Awake(){
		player = target.gameObject.GetComponent<PlayerScript> ();
		lastCheckPoint = new Vector3 (-34.8f, -2.3f, 0f);
		checkHealth = player.playerStats.Health;
//		currKeys = 0;
//		locKeys = GameObject.FindGameObjectsWithTag("Key");
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
//		ScoreManager.numbKeys = currKeys;
//		Debug.Log ("Lenght: " + locKeys.Length);
		// eitthvað rugl í gangi með þetta, testa að færa remove upp í if í update og hafa hitt hérna er næsta hugmynd!!
//		GameObject[] remKey = GameObject.FindGameObjectsWithTag("Key");
//		GameObject[] center = GameObject.FindGameObjectsWithTag("KeyCenter");
//		for (int i = 0; i < remKey.Length; i++) {
//			Destroy(remKey[i]);
//		}
//		for(int i = 0; i < center.Length; i++){
//			Destroy(center[i]);
//		}
//
//		for (int i = 0; i < locKeys.Length; i++) {
//			Debug.Log("Making key number: "+ i);
//			Instantiate(aKey, locKeys[i].transform.position, locKeys[i].transform.rotation);
//		}

	}
	

	void OnTriggerEnter2D (Collider2D obj){
		
		if (obj.name == "Player") {
			lastCheckPoint = obj.transform.position;
			Debug.Log ("Collide:" + obj.transform.position);
			checkHealth = player.playerStats.Health;
//			currKeys = ScoreManager.numbKeys;
//			locKeys = GameObject.FindGameObjectsWithTag("Key");
//			Debug.Log("lyklar: " + locKeys.Length);
		}

	}

}
