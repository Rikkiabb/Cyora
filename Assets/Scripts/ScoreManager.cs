using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {


	Text scoreText; // a text object
	public static int numbKeys; // keys collected
	public static string totalKeys;
	// Use this for initialization


	void Awake(){
		scoreText = GetComponent<Text>(); // Get a reference to the counterText
		numbKeys = 0; // initialize as zero
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Key");
 		totalKeys = arr.Length.ToString();
	}

	
	// Update is called once per frame
	void Update () {

		scoreText.text = numbKeys.ToString() + " / " + totalKeys; // update the score
	}
}
