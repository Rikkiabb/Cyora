using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour {

	public Transform target;
	Animator anim;
	bool hasExited = false;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null && !hasExited){
			// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
			anim.SetTrigger("GameOver");
			hasExited = true;
		}

		if (hasExited) {
			if(Input.GetButtonDown ("Mouse X")){ // R
				Application.LoadLevel(Application.loadedLevel);
			}
			if(Input.GetButtonDown ("Fire2")){ // M
				Application.LoadLevel("MainMenu");
			}
			if(Input.GetButtonDown ("Fire3")){ // Q
				Application.Quit();
			}
		}
	
	}
}
