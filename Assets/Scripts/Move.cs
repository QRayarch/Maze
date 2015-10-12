using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/584736/method-for-grid-movement.html

public class Move : MonoBehaviour {

	// TODO: Change to grid based movement
	public float speed=1.0f;

	private Vector3 endPos;
	private bool moving=false;

	void Start()
	{
		endPos = transform.position;
	}

	protected void move(float dir)
	{
		if (moving && (transform.position == endPos))
			moving = false;

		if (!moving) {

			if (dir > 0) {
				moving=true;
				endPos=transform.position + new Vector3 (1, 0, 0);
			} 

			else if (dir < 0) {
				moving=true;
				endPos=transform.position - new Vector3 (1, 0, 0);
			}
		}

		transform.position = Vector3.MoveTowards (transform.position, endPos, Time.deltaTime * speed);
	}

	/*protected void moveLeft() {
		//float xMovement = Input.GetAxis ("Horizontal");
		transform.position -= new Vector3 (.1f, 0, 0);
	}

	protected void moveRight() {
		//float xMovement = Input.GetAxis ("Horizontal");
		//transform.position += new Vector3 (xMovement, 0, 0);
		transform.position += new Vector3 (.1f, 0, 0);
	}*/

	protected void jump() {
		//float yMovement = Input.GetAxis ("Jump");
		transform.position += new Vector3 (0, .1f, 0);

	}
}
