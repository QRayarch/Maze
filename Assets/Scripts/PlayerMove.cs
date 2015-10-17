using UnityEngine;
using System.Collections;

public class PlayerMove : Move
{

	public bool jumping = false;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

		if (Input.GetAxisRaw ("Jump") > 0) {
			jumping = true;
		} else {
			jumping=false;
		}

		move (Input.GetAxisRaw("Horizontal"),jumping);
	}
}

