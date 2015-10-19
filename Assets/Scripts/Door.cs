using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
	private Animator animator;
	private bool locked = true;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
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
					PlayAnimationTrigger("doorOpen");
				}
			}
		}
		
	}

	private void PlayAnimationTrigger(string trigger) {
		if(animator == null) return;
		animator.SetTrigger(trigger);
	}

}

