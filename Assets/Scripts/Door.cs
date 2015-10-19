using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{

	private bool locked = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.isTrigger) return;
		
		if(!locked) {
			return;
		} else {
			KeyHolder keyHolder = other.gameObject.GetComponent<KeyHolder>();
			if(keyHolder != null) {
				if(keyHolder.hasKey()) {
					locked = false;
				}
			}
		}
		
	}

}

