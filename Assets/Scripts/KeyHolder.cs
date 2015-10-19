using UnityEngine;
using System.Collections;

public class KeyHolder : MonoBehaviour
{

	private bool holdingKey = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void obtainKey() {
		holdingKey = true;
	}

	public bool hasKey() {
		return holdingKey;
	}
}

