using UnityEngine;
using System.Collections;

public class PlayerMove : Move
{
	public float minHoldDownForCancel = 0.4f;

	private float timeMoveFor = 0.0f;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f;
		move (Input.GetAxis("Horizontal"));

		if(isMoving) {
			timeMoveFor += Time.deltaTime;
		} else if(timeMoveFor > 0) {
			if(timeMoveFor >= minHoldDownForCancel && StepUpdater.StepTimer >= 0.2f) {
				Debug.Log("Canceling!");
				CancelMove();
				timeMoveFor = 0;
			}
			//Debug.Log("Stop " + StepUpdater.TimeTillNextStep);
		}


		if (Input.GetAxis ("Jump") > 0) {
			jump();
		}
	}
}

