using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){
//			
			//		var cam : CameraPosition = GetComponent(CameraPosition);
			//		cam.target = camera.transform;
			//TODO: Disable controll script
			CameraPosition.target = transform;
			//TODO: PLay animation
			PlayerScript.finished = true;
			PlayerScript.isMoving = false;
			CanvasController.clearedLevel = true;
			
//			
	}
		
	}
}
