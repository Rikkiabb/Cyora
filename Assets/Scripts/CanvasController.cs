using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour {

	public Transform target;
	public static Animator anim;
	bool hasExited = false;
	static public bool clearedLevel;

	void Awake(){
		clearedLevel = false;
	}
	void Start () {
		anim = GetComponent<Animator> ();
		clearedLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript player = target.gameObject.GetComponent<PlayerScript> ();
		if((player.playerStats.Health < 1) && !hasExited){
			// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
			anim.SetTrigger("GameOver");
			hasExited = true;
//			Physics2D.gravity = new Vector2(0, -30);

		}

		if (player.playerStats.Health > 0 && clearedLevel) {
			// Þurfum að búa til scene hérna
			Physics2D.gravity = new Vector2(0, -30);

		}

		if (hasExited) {
			if(Input.GetButtonDown ("Mouse X")){ // R
				anim.SetTrigger ("Restart");
				hasExited = false;
				if(LevelTwoMaster.windRight){
					Physics2D.gravity = new Vector2(115, -30);
				} else if (LevelTwoMaster.windLeft){
					Physics2D.gravity = new Vector2(-115, -30);
				} else {
					Physics2D.gravity = new Vector2(0, -30);
				}
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
