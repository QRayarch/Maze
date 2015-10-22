using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		KeyHolder keyHolder = other.gameObject.GetComponent<KeyHolder>();
		if(keyHolder != null) {
			if(!keyHolder.hasKey ()) {
				keyHolder.obtainKey();
				Destroy(gameObject);
			}
		}
	}
}
