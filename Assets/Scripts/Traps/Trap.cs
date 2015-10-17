using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour {

	public int damage = 1;

	private Animator animator;
	private bool canActivate = false;
	private bool hasActivated = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hasActivated) {
			if(animator == null || animator.GetCurrentAnimatorStateInfo(0).IsName("Finish")) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.isTrigger) return;
		TryActivateTrap(other.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.collider.isTrigger) return;
		TryActivateTrap(coll.gameObject);
	}

	public void TryActivateTrap(GameObject hit) {
		if(!canActivate) return;
		Health health = hit.GetComponent<Health>();
		if(health == null || health.IsDead) return;
		health.TakeDamage(damage);

		PlayAnimationTrigger("activate");
		hasActivated = true;
	}

	public void Place() {
		PlayAnimationTrigger("placed");
		canActivate = true;
	}

	private void PlayAnimationTrigger(string trigger) {
		if(animator == null) return;
		animator.SetTrigger(trigger);
	}
}
