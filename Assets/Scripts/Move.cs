using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/584736/method-for-grid-movement.html

public class Move : Stepable {
	
	public float speed = 1.0f;

	private bool initMoving = false;
	private bool moving = false;
	private bool jumping = false;

	private Vector3 endPos;

	public virtual void Start()
	{
		endPos = transform.position;
		initMoving = true;
	}

	protected void move(float dir)
	{
		if (moving && (transform.position == endPos))
			moving = false;

		if (!moving) {

			if (dir > 0) {
				moving = true;
				endPos = transform.position + new Vector3 (1, 0, 0);
			} 

			else if (dir < 0) {
				moving = true;
				endPos = transform.position - new Vector3 (1, 0, 0);
			}
		}
	}

	public override void Step()
	{
		if (initMoving) {
			transform.position = endPos;
		}
	}

	protected void jump() {

		if (jumping && (transform.position == endPos))
			jumping = false;
		
		if (!jumping) {
			jumping = true;
			endPos=transform.position + new Vector3 (0, 1, 0);
		}

	}
}
