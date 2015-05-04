using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour {

	public Transform target;
	Animator anim;
	bool hasExited = false;
	static public bool clearedLevel = false;

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

		if (target != null && clearedLevel) {
			anim.SetTrigger("ClearLevel");
			clearedLevel = false;
		}

		if (hasExited) {
			if(Input.GetButtonDown ("Mouse X")){ // R
				PlayerScript.isMoving = true;
				GameMasterCS.numbKeys = 0;
				Application.LoadLevel(Application.loadedLevel);
			}
			if(Input.GetButtonDown ("Fire2")){ // M
				PlayerScript.isMoving = true;
				GameMasterCS.numbKeys = 0;
				Application.LoadLevel("MainMenu");
			}
			if(Input.GetButtonDown ("Fire3")){ // Q
				Application.Quit();
			}
		}
	
	}
}
