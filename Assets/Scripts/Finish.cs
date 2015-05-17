using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
	private AudioSource source;
	public AudioClip levelCleared;
	Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		source = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){
			//mute the theme music
			Camera.main.GetComponent<AudioSource>().mute = true;
			// play the level cleared theme
			source.clip = levelCleared;
			source.Play();
			CameraPosition.target = transform;
			PlayerScript.finished = true;
			PlayerScript.isMoving = false;
			CanvasController.theEnd = true;
			CanvasController.clearedLevel = true;
			anim.SetTrigger("Finished");
			
//			
		}
		
	}
}
