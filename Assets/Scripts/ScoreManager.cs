using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {


	Text scoreText; // a text object
	public static int numbKeys; // keys collected
	// Use this for initialization


	void Awake(){
		scoreText = GetComponent<Text>(); // Get a reference to the counterText
		numbKeys = 0; // initialize as zero
	}

	
	// Update is called once per frame
	void Update () {
		scoreText.text = numbKeys.ToString(); // update the score
	}
}
