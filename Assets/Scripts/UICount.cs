using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICount : MonoBehaviour {

	public Text numTraps;
	int numOfTraps;
	public int index;
	public string prefix;

	private LevelTraps traps;

	// Use this for initialization
	void Start () {
		traps = GameObject.FindObjectOfType<LevelTraps>();
	}
	
	// Update is called once per frame
	void Update () {
		if (traps.trapHolders != null) {
			numTraps.text = "X "+prefix + traps.trapHolders [index].numPerLevel.ToString();
		}
	}
}
