using UnityEngine;
using System.Collections;

public class PlayerMove : Move
{

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		move (Input.GetAxis("Horizontal"));

		/*
		if (Input.GetAxis ("Horizontal") < 0) {
			moveLeft();
		}

		if (Input.GetAxis ("Horizontal") > 0) {
			moveRight();
		}*/

		if (Input.GetAxis ("Jump") > 0) {
			jump();
		}
	}
}

