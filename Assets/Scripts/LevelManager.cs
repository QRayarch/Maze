using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public string levelName = "MichaelDemoLevel";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "StartScreen" && Input.GetKeyDown ("return")) {
			//int i=Application.loadedLevel;
			Application.LoadLevel (levelName);
		}
		if (Application.loadedLevelName == levelName) {
			int numSpawns = GameObject.FindObjectOfType<EnemySpawner>().spawnPoints.Count;
			KeyHolder[] enemies = GameObject.FindObjectsOfType<KeyHolder>();
			
			if(numSpawns == 0 && enemies.Length == 0) {
				// no more spawns & all enemies are dead
				Application.LoadLevel ("WinScreen");
			}

			bool doorLocked = GameObject.FindObjectOfType<Door>().getLocked();
			if(doorLocked == false) {
				Application.LoadLevel ("LoseScreen");
			}

		}

	}

	public void LoadLevel() {
		Application.LoadLevel(levelName);
		//Application.LoadLevel ("WinScreen");
		//Application.LoadLevel ("LoseSceen");
	}
}
