using UnityEngine;
using System.Collections;

public class TutorMaster : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (CanvasController.clearedLevel) {
			Application.LoadLevel("MainMenu");
		}
	}
}
