using UnityEngine;
using System.Collections;

public class StepUpdater : MonoBehaviour {

	[Header("Config")]
	public float stepTime = 0.8f;

	private Stepable[] stepables;
	private float stepTimer;

	// Use this for initialization
	void Start () {
		stepables = GameObject.FindObjectsOfType<Stepable>();
		for(int s = 0; s < stepables.Length; s++) {
			if(stepables[s] != null) {
				stepables[s].StepUpdater = this;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckForStep();
	}

	private void CheckForStep() {
		stepTimer += Time.deltaTime;
		if(stepTimer >= stepTime) {
			StepAll();
		}
	}

	public void StepAll() {
		stepTimer = 0;
		for(int s = 0; s < stepables.Length; s++) {
			if(stepables[s] != null) {
				stepables[s].Step();
			}
		}
	}

	public float TimeTillNextStep {
		get{ return stepTime - stepTimer; }
	}

	public float StepTimer {
		get{ return stepTimer; }
	}
}
