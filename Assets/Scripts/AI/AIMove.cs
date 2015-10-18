using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathFinder))]
public class AIMove : Move {

	[Header("Pathfinding Config")]
	public int heightCanJump = 3;
	public int distanceCanJump = 3;
	public int maxSearchDistance = 40;

	private PathFinder pathFinder;
	private Transform trans;
	private int posX;
	private int posY;
	private PathFinder.Path path;
	private int currentNodeIndex = 0;

	// Use this for initialization
	void Start () {
		base.Start();

		pathFinder = GetComponent<PathFinder>();
		pathFinder.Grid = grid;
		pathFinder.DistanceCanJump = heightCanJump;
		pathFinder.HeightCanJump = distanceCanJump;
		pathFinder.MaxDistance = maxSearchDistance;
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {

		posX = (int)(trans.position.x);
		posY = (int)(trans.position.y);

		if(path != null) {
			PathFinder.Node currentNode = path.nodes[currentNodeIndex];
			float dir = currentNode.posX - posX;
			bool isJump = false;
			
			//Next node is the last
			if(currentNodeIndex + 1 == path.Distance) {
				//We are at the end, complet
				if(posX == currentNode.posX && posY == currentNode.posY) {
					path = null;
					currentNodeIndex = 0;
					Debug.Log("Finished");
				}
			} else {
				PathFinder.Node nextNode = path.nodes[currentNodeIndex + 1];
				isJump = nextNode.isJump;
				if(posX == currentNode.posX && posY == currentNode.posY) {
					currentNodeIndex++;
				}
			}
			if(isJump) {
				Debug.Log("Dir " + dir + " " + posX + " " + posY + " " + currentNode.posX + " " + currentNode.posY);
			}
			move(dir, isJump);
		}

		if(pathFinder != null) {
			if(Input.GetMouseButton(0)) {
				if(Camera.current != null) {
					path = pathFinder.FindPath(trans.position, Camera.current.ScreenToWorldPoint(Input.mousePosition));
					currentNodeIndex = 0;
				}
			}
			pathFinder.DebugDrawPath(path);
		}
	}
}
