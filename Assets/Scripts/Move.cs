using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/584736/method-for-grid-movement.html

public class Move : MonoBehaviour {

	protected Grid grid;

	private int speed=5;
	private float jumpForce=700f;

	float x;

	public virtual void Start()
	{
		x = 0;
		grid = GameObject.FindObjectOfType<Grid>();

	}

	protected void move(float dir, bool jumping)
	{
		if (dir > 0) {
			x = Vector2.right.x * speed;
		} else if (dir < 0) {
			x = Vector2.left.x * speed;
		} else if (dir == 0) {
			x=0;
		}

		if(jumping){
			gameObject.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0, jumpForce));
		}

//		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(x, gameObject.GetComponent<Rigidbody2D> ().velocity.y);
	}		
}
