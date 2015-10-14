using UnityEngine;
using System.Collections;

public class PlayerMove : Move
{

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		move (Input.GetAxis("Horizontal"));

		if (Input.GetAxis ("Jump") > 0) {
			jump();
		}
	}
}

