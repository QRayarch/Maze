using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/584736/method-for-grid-movement.html

public class GridMove : Stepable {
	
	public float speed = 1.0f;
	public int jumpHeight = 3;
	public int jumpDistance = 3;

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
	
	protected void move(float dir, bool j)
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
			if(dir != 0) {
				Vector3 scale = transform.localScale;
				scale.x = Mathf.Abs(scale.x) * Mathf.Sign(dir);
				transform.localScale = scale;
			}
		}
		if(j) {
			jump(dir);
		}
	}
	
	public override void Step()
	{
		if (initMoving) {
			int x = (int) (transform.position.x - .5);
			int y = (int) (transform.position.y - .5);
			
			while(!grid.IsGridSpaceCollidable(x, y - 1) && y > 0) {
				endPos += Vector3.down;
				y--;
			}
			StartCoroutine(TransitionMovment(StepUpdater.stepTime / 2, endPos));
			jumping = false;
			//transform.position = endPos;
		}
		
	}
	
	private void jump(float dir) {
		
		/*if (jumping && (transform.position == endPos))
			jumping = false;*/
		
		if (!jumping) {
			Debug.Log("JUMP!");
			jumping = true;
			
			int x = (int) (transform.position.x - .5);
			int y = (int) (transform.position.y - .5);
			
			int jumpHeight = 0;
			int d = (int)Mathf.Sign(transform.localScale.x);

			for(int j = 1; j <= 3; j++) {
				if(!grid.IsGridSpaceCollidable(x, y + j)) {
					jumpHeight++;
				} else {
					j = 10;
				}
			}
			endPos += new Vector3 (d, jumpHeight, 0);
			Debug.Log(endPos);
		}
	}
	
	protected void CancelMove() {
		StopCoroutine("TransitionMovment");
		int x = (int) (transform.position.x - .5);
		int y = (int) (transform.position.y - .5);
		endPos = new Vector3(x + 0.5f, y + 0.5f, 0.0f);
		transform.position = endPos;
	}
	
	IEnumerator TransitionMovment(float t, Vector3 newPos) {
		float time = 0;
		Transform trans = transform;
		Vector3 startPos = trans.position;
		while(time < t) {
			time += Time.deltaTime;
			trans.position = Vector3.Lerp(startPos, newPos, time / t);
			yield return null;
		}
		trans.position = newPos;
		endPos = transform.position;
	}
}
