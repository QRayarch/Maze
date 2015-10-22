using UnityEngine;
using System.Collections;

public class KeyHolder : MonoBehaviour
{
	public GameObject keyIndicator;
	private bool holdingKey = false;
	//public GameObject key;

	// Use this for initialization
	void Start ()
	{
		if(keyIndicator != null) {
			keyIndicator.SetActive(false);
		}
		GetComponent<Health>().OnDeath += dropKey;
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

	public void dropKey() {
		//Instantiate (key, transform.position, Quaternion.identity);
	}

}

