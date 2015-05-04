using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {
	public Transform realTarget;
	public static Transform target;
	
	void Start(){
		target = realTarget;
	}
	
	void Update () {
		if(target != null){
			// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
			transform.position = target.position + new Vector3(0, -target.position.y, -10);
			realTarget = target;
		}
	}
}
