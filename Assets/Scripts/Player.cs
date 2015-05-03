using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Create a new player stat class which handles his health and weapons
	[System.Serializable]
	public class PlayerStats {
		public int Health = 3;
	}
 
	// instantiate
	public PlayerStats playerStats = new PlayerStats();
	// Deal a damage to this player
	public int fallBoundary = -5;

	void Update(){
		if (transform.position.y <= fallBoundary){
			Debug.Log ("Player fell to his death");
			DamagePlayer(9999);
		}
	}


	public void DamagePlayer (int damage){
		playerStats.Health -= damage;
		// So if our player empties his health he dies
		if(playerStats.Health <= 0){
			Debug.Log("Kill Player!!");
			GameMasterCS.KillPlayer(this);

		}
	}

}
