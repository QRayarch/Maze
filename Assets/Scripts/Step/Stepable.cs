using UnityEngine;
using System.Collections;

public abstract class Stepable : MonoBehaviour {
	private StepUpdater updater;
	public abstract void Step();

	public StepUpdater StepUpdater {
		get{ return updater; }
		set{ updater = value; }
	}
}
