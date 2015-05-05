using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){

			CameraPosition.target = transform;
			PlayerScript.finished = true;
			PlayerScript.isMoving = false;
			CanvasController.clearedLevel = true;
			
//			
	}
		
	}
}
