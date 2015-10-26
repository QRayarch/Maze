using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName=="StartScreen"&&Input.GetKeyDown ("return")) {
			//int i=Application.loadedLevel;
			Application.LoadLevel("MichaelDemoLevel");
		}
		if (Application.loadedLevelName == "MichaelDemoLevel" && GetComponent<EnemySpawner> ().spawnPoints.Count == 0) {
			Application.LoadLevel ("WinScreen");
		}
	}

	public void LoadLevel() {
		Application.LoadLevel("MichaelDemoLevel");
	}
}
