using UnityEngine;
using System.Collections;

public class LevelTwoMaster : MonoBehaviour {

	bool windLeft, windRight;
	int placement;

	void Start(){
		windLeft = false;
		windRight = false;
		placement = 0;
	}


	void OnTriggerEnter2D (Collider2D obj){
		if (obj.name == "Player") {
		
			if(!windLeft && placement == 0){
				windLeft = true;
				placement++;
				Physics2D.gravity = new Vector2(-115, -30);
			}

			if(!windRight && placement == 1){
				windRight = true;
				placement++;
				Physics2D.gravity = new Vector2(115, -30);
				GameObject explode = GameObject.FindGameObjectWithTag("Explode");
				Destroy(explode);
			}
		
		}
	}
}
