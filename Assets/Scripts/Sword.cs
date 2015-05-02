using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

//	private Animator anim;
//	float rotSpeed = 60;
	private float swingDuration = 10f;
	private float swingSpeed = 0.22f;
	
	private float swingTimer = 0f;
	private bool swinging = false;
	private Vector3 startRot;

	// Use this for initialization
	void Start () {

		//Get the animator for the sword.
//		anim = GetComponent<Animator> ();
		startRot = transform.eulerAngles;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1") && !swinging) {
//			transform.Rotate(90, rotSpeed * Time.deltaTime, 0, Space.World);
			//anim.SetTrigger("Swing");
//			transform.rotation = Quaternion.AngleAxis(90, Vector3.back);
//			transform.Rotate(0,0,-90);
//			transform.Rotate(Vector3.back, Time.deltaTime * 100000, Space.Self);
//			transform.eulerAngles = Vector3(0, 0, 30);
//			Swing ();
			swinging = true;

		}
			
		
		if (swinging) {
			swingTimer += Time.deltaTime;
			
			if (swingTimer < (swingDuration / 2)) {
				transform.eulerAngles = Vector3.Lerp(startRot, new Vector3(0, 0, 1), swingSpeed);
			}
			
			if (swingTimer > (swingDuration / 2)) {
				transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, startRot, swingSpeed);
			}
			
			if (swingTimer > swingDuration) {
				swingTimer = 0f;
				swinging = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D obj){

//		anim.SetTrigger("Swing");
		if (obj.tag == "Enemy") {
			Destroy(obj.gameObject);
		}
	}

	void Swing(){
		transform.Rotate (0, 0, 90);
	}
}
