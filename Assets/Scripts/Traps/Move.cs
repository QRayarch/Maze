using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/584736/method-for-grid-movement.html

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour {

	protected Grid grid;

	public int speed=5;
	public float jumpForce=100f;
	public bool grounded=false;
	public Transform groundCheck;
	float groundRad=.2f;
	public LayerMask ground;
	public bool facingRight=true;


	float x;

	public virtual void Start()
	{
		x = 0;
		grid = GameObject.FindObjectOfType<Grid>();

	}

	protected void move(float dir, bool jumping)
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRad, ground);

		if (dir > 0) {
			x = Vector2.right.x * speed;
		} else if (dir < 0) {

			x = Vector2.left.x * speed;
		} else if (dir == 0) {
			x=0;
		}

		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(x, gameObject.GetComponent<Rigidbody2D> ().velocity.y);

		if(jumping&&grounded){
			gameObject.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0, jumpForce));
		}
	}
	
}
