using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	private int maxDistance;
	private Grid grid;
	private int heightCanJump;
	private int distanceCanJump;

	public struct Node {
		public int posX;
		public int posY;
		public bool shouldJump;//Defaults to false
	}

	private enum Direction {
		Left = -1,
		Right = 1,
	}

	public class Path {
		public bool didFindTarget = false;
		public List<Node> nodes = new List<Node>();

		public void Add(Node n) {
			nodes.Add(n);
		}

		public int Distance {
			get{ return nodes.Count; }
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Path FindPath(Vector3 start, Vector3 target) {
		Node startN = new Node();
		startN.posX = Mathf.RoundToInt(start.x);
		startN.posY = Mathf.RoundToInt(start.y);

		int targetX = Mathf.RoundToInt(target.x);
		int targetY = Mathf.RoundToInt(target.y);

		Path lPath = FindPath(new Path(), startN, Direction.Left, targetX, targetY, 0);
		Path rPath = FindPath(new Path(), startN, Direction.Right, targetX, targetY, 0);

		return ComparePaths(lPath, rPath);
	}

	private Path FindPath(Path p, Node cur, Direction dir, int tarX, int tarY, int distance) {
		//We found the target return the path
		if(cur.posX == tarX && cur.posY == tarY) {
			return p;
		}
		//Looked to far and didn't find the target
		if(distance >= maxDistance) {
			p.didFindTarget = false;
			return p;
		}

		//Add the current position to the path
		p.Add(cur);

		//Check what we can do
		bool isPitfall = IsPitfallInFrontOf(cur.posX, cur.posY, dir);
		bool canForward = CanMoveForward(cur.posX, cur.posY, dir);
		bool isHorizJump = IsHorizontalJumpAvalible(cur.posX, cur.posY, dir);
		bool isVertJump = IsVerticalJumpAvalible(cur.posX, cur.posY, dir);
		
		if(canForward && !isPitfall) {
			Node forward = new Node();
			forward.posX = cur.posX + (int)dir;
			forward.posY = cur.posY;
			return FindPath(p, forward, dir, tarX, tarY, distance + 1);
		}


		return p;
	}

	private Path ComparePaths(Path p1, Path p2) {
		//Check to see if both paths found a path, if not return the one that found a path
		if(p1.didFindTarget && !p2.didFindTarget) {
			return p1;
		}
		if(p2.didFindTarget && !p1.didFindTarget) {
			return p2;
		}
		
		//Return the shortest path
		if(p1.Distance < p2.Distance) {
			return p1;
		}
		return p2;
	}

	private bool IsPitfallInFrontOf(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x += (int)dir;
		y += 1;
		return grid.IsInGridBounds(x, y) && !grid.IsGridSpaceCollidable(x, y);
	}

	private bool CanMoveForward(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x+= (int)dir;
		return grid.IsInGridBounds(x, y) && !grid.IsGridSpaceCollidable(x, y);
	}

	private bool IsHorizontalJumpAvalible(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x += ((int)dir) * distanceCanJump;
		if(!grid.IsInGridBounds(x, y) || grid.IsGridSpaceCollidable(x, y)) return false;
		y += 1;
		return grid.IsInGridBounds(x, y) && grid.IsGridSpaceCollidable(x, y);
	}

	private bool IsVerticalJumpAvalible(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x += (int)dir;
		y -= heightCanJump;
		if(!grid.IsInGridBounds(x, y) || grid.IsGridSpaceCollidable(x, y)) return false;
		y += 1;
		return grid.IsInGridBounds(x, y) && grid.IsGridSpaceCollidable(x, y);
	}

	public int MaxDistance {
		set{ maxDistance = value; }
	}

	public Grid Grid {
		set{ grid = value; }
	}

	public int HeightCanJump {
		set{ heightCanJump = value; }
	}

	public int DistanceCanJump {
		set{ distanceCanJump = value; }
	}
}
