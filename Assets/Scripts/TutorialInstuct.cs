using UnityEngine;
using System.Collections;

public class TutorialInstuct : MonoBehaviour {

	int introText = 0;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player" && introText == 0){
			anim.SetBool("Instruct1", true);
			StartCoroutine (waitText ());
		}

		if(obj.name == "Player" && introText == 1){
			anim.SetBool("Instruct1", true);
			StartCoroutine (waitText ());
		}

		if(obj.name == "Player" && introText == 2){
			anim.SetBool("Instruct1", true);
			StartCoroutine (waitText ());
		}
	}

	IEnumerator waitText(){

		yield return new WaitForSeconds (2);
		introText++;

	}
	
}