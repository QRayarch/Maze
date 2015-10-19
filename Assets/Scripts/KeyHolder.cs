using UnityEngine;
using System.Collections;

public class KeyHolder : MonoBehaviour
{
	public GameObject keyIndicator;
	private bool holdingKey = false;

	// Use this for initialization
	void Start ()
	{
		if(keyIndicator != null) {
			keyIndicator.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void obtainKey() {
		holdingKey = true;
		keyIndicator.SetActive(true);
	}

	public bool hasKey() {
		return holdingKey;
	}
}

