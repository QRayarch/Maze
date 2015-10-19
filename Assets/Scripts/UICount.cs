using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICount : MonoBehaviour {

	public Text numTraps;
	int numOfTraps;
	public int index;
	public LevelTraps traps;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (traps.trapHolders != null) {
			numTraps.text = traps.trapHolders [index].numPerLevel.ToString();
		}
	}
}
