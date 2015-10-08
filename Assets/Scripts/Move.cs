using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// TODO: Change to grid based movement

	protected void moveLeft() {
		float xMovement = Input.GetAxis ("Horizontal");
		transform.position += new Vector3 (xMovement, 0, 0);
	}

	protected void moveRight() {
		//float xMovement = Input.GetAxis ("Horizontal");
		//transform.position += new Vector3 (xMovement, 0, 0);
		transform.position += new Vector3 (.016666667f, 0, 0);
	}

	protected void jump() {
		float yMovement = Input.GetAxis ("Jump");
		transform.position += new Vector3 (0, yMovement, 0);
	}
}
