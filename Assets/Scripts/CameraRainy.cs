using UnityEngine;
using System.Collections;

public class CameraRainy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (RainyStart.started) {
			Vector3 temp = transform.position;
			temp.y += 0.07f;
			transform.position = temp;
		}


	}
}
