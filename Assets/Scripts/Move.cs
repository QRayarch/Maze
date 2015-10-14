using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/584736/method-for-grid-movement.html

public class Move : Stepable {
	
	public float speed = 1.0f;

	private bool initMoving = false;
	private bool moving = false;
	private bool jumping = false;

	private Vector3 endPos;

	protected Grid grid;
	
	public virtual void Start()
	{
		endPos = transform.position;
		initMoving = true;
		grid = GameObject.FindObjectOfType<Grid>();
	}

	protected void move(float dir)
	{
		if (moving && (transform.position == endPos))
			moving = false;

		if (!moving) {

			int x = (int) (transform.position.x - .5);
			int y = (int) (transform.position.y - .5);

			if (dir > 0 && !grid.IsGridSpaceCollidable(x + 1, y)) {
				moving = true;
				endPos = transform.position + Vector3.right;
			} 

			else if (dir < 0 && !grid.IsGridSpaceCollidable(x - 1, y)) {
				moving = true;
				endPos = transform.position + Vector3.left;
			}

		}
	}

	public override void Step()
	{
		if (initMoving) {
			transform.position = endPos;
			endPos = transform.position;

			int x = (int) (transform.position.x - .5);
			int y = (int) (transform.position.y - .5);
			
			while(!grid.IsGridSpaceCollidable(x, y - 1) && y >= 0) {
				endPos += Vector3.down;
				y--;
			}
		}

	}

	protected void jump() {

		if (jumping && (transform.position == endPos))
			jumping = false;
		
		if (!jumping) {
			jumping = true;
			endPos=transform.position + new Vector3 (0, 3, 0);
		}

	}
}
