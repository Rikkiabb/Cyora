using UnityEngine;
using System.Collections;

public class IcyIntro2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (WaitScene ());
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Icy");
		}
	}

	IEnumerator WaitScene(){
		yield return new WaitForSeconds (11.7f);
		Application.LoadLevel("Icy");
	}
}
