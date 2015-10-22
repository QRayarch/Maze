using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Hurt : MonoBehaviour {

	public int damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.isTrigger) return;
		HurtHealth(other.gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.collider.isTrigger) return;
		HurtHealth(coll.gameObject);
	}

	private void HurtHealth(GameObject hit) {
		Health health = hit.GetComponent<Health>();
		if(health == null || health.IsDead) return;
		health.TakeDamage(damage);
	}
}
