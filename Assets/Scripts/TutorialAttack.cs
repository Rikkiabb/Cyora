using UnityEngine;
using System.Collections;

public class TutorialAttack : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	
	void Update () {
		if (Input.GetButtonDown ("Fire1") && !anim.GetBool ("isKnight3Attacking")) {
			
			anim.SetBool("isKnight3Attacking", true);
			StartCoroutine (waitAttack ());
		}

	}

	IEnumerator waitAttack(){
		
		yield return new WaitForSeconds (0.5f);
		
		anim.SetBool("isKnight3Attacking", false);
	}
}
