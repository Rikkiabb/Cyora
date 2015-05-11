using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class StartMaster : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		// Original settings of player
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData();
		data.heal = 3;
		data.jf = 1410;
		data.hdj = false;
		data.swordSizeX = 1.3f;
		data.swordSizeY = 1.3f;
		bf.Serialize(file, data);
		file.Close ();
	}

}
