using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMasterCS : MonoBehaviour {

	//public static int numbKeys;
	
	public int offsetY = 40;
	public int offsetX = 40;
	public int sizeX = 100;
	public int sizeY = 40;
	public static bool icy = false;
	public static void setIce(bool ice){
		icy = ice;
	}
	
	public static bool isIcy(){
		return icy;
	}
	
	void OnGUI () {
		//GUI.Box(new Rect (offsetX, offsetY, sizeX, sizeY), "Keys\n" + numbKeys );
	}
	
	// This class takes cares of adding and removing the players and enemies
	
	// This function destroys the main player
	// This is a static function because we want to be able to kill from everywhere
	public static void KillPlayer(PlayerScript player){
		Destroy(player.gameObject);
	}
	
	
	public static void KillEnemy(Enemy enemy){
		Destroy(enemy.gameObject);
	}

	public void StopFinal(string stage){
		GameObject b = GameObject.Find ("Boss");
		GameObject[] eBalls = GameObject.FindGameObjectsWithTag ("Ball");
		PlayerScript player = GameObject.Find ("Player").GetComponent<PlayerScript> ();
		BossAI[] boss = b.GetComponentsInChildren<BossAI>();
		Animator anim = b.GetComponent<Animator> ();
		Debug.Log (boss.Length);

		for (int i = 0; i < eBalls.Length; i++) {
			Destroy (eBalls[i].gameObject);
		}

		player.enabled = false;
		boss[0].enabled = false;

		anim.SetBool("Stage2", true);
		StartCoroutine (WaitAnim (anim, stage));

	}


	IEnumerator WaitAnim(Animator anim, string stage){
		BossAI[] boss = GameObject.Find ("Boss").GetComponentsInChildren<BossAI>();
		yield return new WaitForSeconds (4);
		GameObject.Find ("Player").GetComponent<PlayerScript> ().enabled = true;
		if (stage == "stage2") {
			boss [0].Stage2 ();
			GameObject.Find ("WIND").GetComponent<Animator> ().SetBool ("Wind", true);
			StartCoroutine (WaitText ());
		} else if (stage == "stage3") {
			boss [0].Stage3 ();
			GameObject.Find ("ICE").GetComponent<Animator> ().SetBool ("Ice", true);
			StartCoroutine (WaitText ());
		} else if (stage == "stage4") {
			GameObject.Find ("ENEMIES").GetComponent<Animator>().SetBool("Enemies", true);
			StartCoroutine(WaitText());
		}
		yield return new WaitForSeconds (4);
		anim.SetBool ("Stage2", false);

		if (stage == "stage4") {
			boss[0].Stage4();
		}
		yield return new WaitForSeconds (1);

		boss [0].enabled = true;
	}

	IEnumerator WaitText(){

		yield return new WaitForSeconds (4);
		GameObject.Find ("WIND").GetComponent<Animator>().SetBool("Wind", false);
		GameObject.Find ("ICE").GetComponent<Animator>().SetBool("Ice", false);
		GameObject.Find ("ENEMIES").GetComponent<Animator>().SetBool("Enemies", false);
	}
}
