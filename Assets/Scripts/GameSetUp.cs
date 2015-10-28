using UnityEngine;
using System.Collections;

public class GameSetUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Chest[] chests = GameObject.FindObjectsOfType<Chest>();
		chests[Random.Range(0, chests.Length)].hasKey = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
