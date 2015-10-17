using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	private int maxDistance;
	private Grid grid;

	public struct Node {
		public int posX;
		public int posY;
		public int distance;
	}

	private enum Direction {
		Left = -1,
		Right = 1,
	}

	public struct Path {
		public bool didFindTarget = false;
		public List<Node> nodes = new List<Node>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Path FindPath(Vector3 start, Vector3 target) {
		Node startN = new Node();
		startN.distance = 0;
		startN.posX = Mathf.RoundToInt(start.x);
		startN.posY = Mathf.RoundToInt(start.y);

		int targetX = Mathf.RoundToInt(target.x);
		int targetY = Mathf.RoundToInt(target.y);

		Path lPath = FindPath(new Path(), startN, Direction.Left, targetX, targetY, 0);
		Path rPath = FindPath(new Path(), startN, Direction.Right, targetX, targetY, 0);

		return lPath;
	}

	private Path FindPath(Path p, Node start, Direction dir, int tarX, int tarY, int distance) {

	}

	public int MaxDistance {
		set{ maxDistance = value; }
	}

	public Grid Grid {
		set{ grid = value; }
	}
}
