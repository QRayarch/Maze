using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Chest : MonoBehaviour {

	private Animator animator;
	private bool opened;
	public bool hasKey = false;

	// Use this for initialization
	void Start () {
		opened = false;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {

		if(opened) {
			return;
		} else {
			KeyHolder keyHolder = other.gameObject.GetComponent<KeyHolder>();
			if(keyHolder != null) {
				if(!keyHolder.hasKey ()) {
					PlayAnimationTrigger("chestOpen");
					
					if(hasKey) {
						keyHolder.obtainKey();
					}
					opened = true;
				}
			}

		}

	}

	private void PlayAnimationTrigger(string trigger) {
		if(animator == null) return;
		animator.SetTrigger(trigger);
	}

}
