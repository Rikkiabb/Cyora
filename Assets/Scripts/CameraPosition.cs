using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {
	public Transform realTarget;
	public static Transform target;
	bool movedUp = false;
	int i = 0;
	
	void Start(){
		target = realTarget;
	}
	
	void Update () {
		if(target != null){
			if(target.position.y < 10){
				// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
				transform.position = target.position + new Vector3(0, -target.position.y, -10);
			} else if(!movedUp) {
				for(; i < 21; i++){
					StartCoroutine (waitMove ());
				}
//				movedUp = true;
			}
//			} else {
//				transform.position = target.position + new Vector3(0, -target.position.y + 20, -10);
//			}
			realTarget = target;
		}
	}

	IEnumerator waitMove(){
		yield return new WaitForSeconds (20);
		transform.position = target.position + new Vector3(0, -target.position.y + i, -10);
	}
	
	
}
