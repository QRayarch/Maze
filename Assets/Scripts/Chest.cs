using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Chest : MonoBehaviour {

	private Animator animator;
	private bool opened;
	private bool hasKey;

	// Use this for initialization
	void Start () {
		opened = false;
		hasKey = (Random.value > 0.5f);
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.isTrigger) return;

		if(opened) {
			return;
		} else {
			opened = true;
			PlayAnimationTrigger("chestOpen");
			if(hasKey) {
				KeyHolder keyHolder = other.gameObject.GetComponent<KeyHolder>();
				if(keyHolder != null) {
					keyHolder.obtainKey();
					hasKey = false;
				}
			}
		}

	}

	private void PlayAnimationTrigger(string trigger) {
		if(animator == null) return;
		animator.SetTrigger(trigger);
	}

}
