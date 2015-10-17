using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DestroyOnDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Health>().OnDeath += DestroyGameObject;
	}
	
	void DestroyGameObject() {
		Destroy(gameObject);
	}
}
