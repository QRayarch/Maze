using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DestroyOnDeath : MonoBehaviour {

	public GameObject spawnOnDeath;

	// Use this for initialization
	void Start () {
		GetComponent<Health>().OnDeath += DestroyGameObject;
	}
	
	void DestroyGameObject() {
		if(spawnOnDeath) {
			Instantiate(spawnOnDeath, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
}
