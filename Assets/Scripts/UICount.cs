using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICount : MonoBehaviour {

	public Text numTraps;
	public Slider slider;
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
			numTraps.text = prefix + traps.trapHolders[index].numPerLevel.ToString();
			slider.value = traps.trapHolders[index].CoolDownPercent;
			slider.gameObject.SetActive(traps.trapHolders[index].numPerLevel > 0);
		}
	}
}
