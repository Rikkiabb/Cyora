using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour {

	public Transform target;
	Animator anim;
	bool hasExited = false;
	static public bool clearedLevel;

	void Awake(){
		clearedLevel = false;
	}
	void Start () {
		anim = GetComponent<Animator> ();clearedLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript player = target.gameObject.GetComponent<PlayerScript> ();
		if((player.playerStats.Health < 1) && !hasExited){
			// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
			anim.SetTrigger("GameOver");
			hasExited = true;
			Physics2D.gravity = new Vector2(0, -30);
//			LevelOneMaster l1 = gameObject.GetComponent<LevelOneMaster>();
//			l1.playerPos();

		}

		if (player.playerStats.Health > 0 && clearedLevel) {
			anim.SetTrigger("ClearLevel");
			clearedLevel = false;
			Physics2D.gravity = new Vector2(0, -30);

			PlayerScript playa = target.gameObject.GetComponent<PlayerScript>();
			playa.setHealth(3);
			playa.setJumpForce(1410);


			GameObject sword = GameObject.FindGameObjectWithTag("Sword");
			sword.transform.localScale = new Vector3(1, 1, 1);
		}

		if (hasExited) {
			if(Input.GetButtonDown ("Mouse X")){ // R
				anim.SetTrigger ("Restart");
				hasExited = false;
//				ScoreManager.numbKeys = 0;
//				Application.LoadLevel(Application.loadedLevel);
			}
			if(Input.GetButtonDown ("Fire2")){ // M
				PlayerScript.isMoving = true;
				ScoreManager.numbKeys = 0;
				Application.LoadLevel("MainMenu");
			}
			if(Input.GetButtonDown ("Fire3")){ // Q
				Application.Quit();
			}
		}
	
	}


	
}
