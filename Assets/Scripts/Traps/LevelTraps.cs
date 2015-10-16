using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTraps : MonoBehaviour {


	public List<TrapHolder> trapHolders;

	[System.Serializable]
	public struct TrapHolder {
		public int numPerLevel;
		public Trap trap;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
