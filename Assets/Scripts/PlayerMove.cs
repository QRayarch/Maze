using UnityEngine;
using System.Collections;

public class PlayerMove : Move
{

	private bool jumping = false;

	private bool grounded=false;
	public Transform groundCheck;
	float groundRad=.2f;
	public LayerMask whatIsGround;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRad, whatIsGround);

		if (Input.GetAxis ("Jump") > 0&&grounded) {
			jumping = true;
		} else {
			jumping=false;
		}
		move (Input.GetAxis("Horizontal"),jumping);
	

	}
}

