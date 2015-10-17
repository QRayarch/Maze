﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	private int maxDistance;
	private Grid grid;
	private int heightCanJump;
	private int distanceCanJump;

	List<Path> dPath = new List<Path>();

	public struct Node {
		public int posX;
		public int posY;
		public bool isJump;//Defaults to false
	}

	private enum Direction {
		Left = -1,
		Right = 1,
	}

	public class Path {
		public bool didFindTarget = false;
		public List<Node> nodes = new List<Node>();

		public Path() {

		}

		public Path(Path p) {
			for(int n = 0; n < p.nodes.Count; n++) {
				nodes.Add(p.nodes[n]);
			}
		}

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

	public void DebugDrawAllPaths() {
		for(int p = 0; p < dPath.Count; p++) {
			DebugDrawPath(dPath[p]);
		}
	}

	public void DebugDrawPath(Path p) {
		if(p == null) return;
		Color renderColor = Color.green;
		//I wanted orange
		if(!p.didFindTarget) renderColor = Color.Lerp(Color.red, Color.yellow, 0.5f);
		for(int n = 0; n < p.nodes.Count; n++) {
			Vector3 loc = new Vector3(p.nodes[n].posX + 0.5f, p.nodes[n].posY + 0.5f, 0);
			int next = n + 1;
			if(next >= p.nodes.Count) {
				Debug.DrawLine(loc, loc + Vector3.up * 0.25f, renderColor);
				Debug.DrawLine(loc, loc + Vector3.down * 0.25f, renderColor);
			} else {
				Vector3 nextLoc = new Vector3(p.nodes[next].posX + 0.5f, p.nodes[next].posY + 0.5f, 0);
				if(p.nodes[next].isJump) {
					int res = 12;
					float radsPerRes = Mathf.PI / res;
					Vector3 prevLoc = loc;
					int dir = -(int)Mathf.Sign((nextLoc.x - loc.y));
					Vector3 center = (nextLoc - loc) / 2 + loc;
					for(int r = 0; r < res; r++) {
						float ang = radsPerRes * r;
						Vector3 jumpLoc = new Vector3(Mathf.Cos(ang) * distanceCanJump / 2 * dir, Mathf.Max(Mathf.Sin(ang) * heightCanJump - 1, loc.y - center.y), 0)  + center;
						Debug.DrawLine(prevLoc, jumpLoc, renderColor);
						prevLoc = jumpLoc;
					}
					Debug.DrawLine(prevLoc, nextLoc, renderColor);
				} else {
					Debug.DrawLine(loc, nextLoc, renderColor);
				}
			}
		}
	}

	public Path FindPath(Vector3 start, Vector3 target) {
		dPath.Clear();
		Node startN = new Node();
		startN.posX = (int)(start.x);
		startN.posY = (int)(start.y);

		int targetX = (int)(target.x);
		int targetY = (int)(target.y);

		Path lPath = FindPath(new Path(), startN, Direction.Left, targetX, targetY, 0);
		Path rPath = FindPath(new Path(), startN, Direction.Right, targetX, targetY, 0);

		return ComparePaths(lPath, rPath);
	}

	private Path FindPath(Path p, Node cur, Direction dir, int tarX, int tarY, int distance) {
		//Add the current position to the path
		p.Add(cur);
		
		//We found the target return the path
		if(cur.posX == tarX && cur.posY == tarY) {
			p.didFindTarget = true;
			return p;
		}
		//Looked to far and didn't find the target
		if(distance >= maxDistance) {
			p.didFindTarget = false;
			return p;
		}

		dPath.Add(p);

		//Check what we can do
		bool isPitfall = IsPitfallInFrontOf(cur.posX, cur.posY, dir);
		bool canForward = CanMoveForward(cur.posX, cur.posY, dir);
		bool isHorizJump = IsHorizontalJumpAvalible(cur.posX, cur.posY, dir);
		bool isVertJump = IsVerticalJumpAvalible(cur.posX, cur.posY, dir);

		Path[] potentialPaths = new Path[4];

		//Generate paths with what we can do
		if(canForward && isPitfall) {
			Node fallen = new Node();
			fallen.posX = cur.posX + (int)dir;
			fallen.posY = FallTillGround(fallen.posX, cur.posY);
			
			Path lPath = FindPath(new Path(p), fallen, Direction.Left, tarX, tarY, distance + 1);
			Path rPath = FindPath(new Path(p), fallen, Direction.Right, tarX, tarY, distance + 1);
			potentialPaths[0] = ComparePaths(lPath, rPath);
		}

		if(canForward && !isPitfall) {
			Node forward = new Node();
			forward.posX = cur.posX + (int)dir;
			forward.posY = cur.posY;
			potentialPaths[1] = FindPath(p, forward, dir, tarX, tarY, distance + 1);
		}

		if(canForward && isPitfall && isHorizJump) {
			Node jump = new Node();
			jump.posX = cur.posX + (int)dir * distanceCanJump;
			jump.posY = cur.posY;
			jump.isJump = true;

			potentialPaths[2] = FindPath(p, jump, dir, tarX, tarY, distance + 1);
		}

		if((!canForward && isVertJump) || isVertJump) {
			Node jump = new Node();
			jump.posX = cur.posX + (int)dir;
			jump.posY = cur.posY + heightCanJump;
			jump.isJump = true;
			
			Path lPath = FindPath(new Path(p), jump, Direction.Left, tarX, tarY, distance + 1);
			Path rPath = FindPath(new Path(p), jump, Direction.Right, tarX, tarY, distance + 1);
			potentialPaths[3] = ComparePaths(lPath, rPath);
		}

		//Find the best path of the options and return it
		Path returnPath = null;
		for(int w = 0; w < potentialPaths.Length; w++) {
			returnPath = ComparePaths(returnPath, potentialPaths[w]);
		}
		return returnPath;
	}

	private Path ComparePaths(Path p1, Path p2) {
		//Check to see if Paths are null
		if(p1 == null && p2 == null) {
			return null;
		}
		if(p1 != null && p2 == null) {
			return p1;
		}
		if(p2 != null && p1 == null) {
			return p2;
		}

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

	private int FallTillGround(int x, int y) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return y;
		while(y >= 0 && grid.IsInGridBounds(x, y - 1) && !grid.IsGridSpaceCollidable(x, y - 1)) {
			y--;
		}
		return y;
	}

	private bool IsPitfallInFrontOf(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x += (int)dir;
		y -= 1;
		return grid.IsInGridBounds(x, y) && !grid.IsGridSpaceCollidable(x, y);
	}

	private bool CanMoveForward(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x+= (int)dir;
		return grid.IsInGridBounds(x, y) && !grid.IsGridSpaceCollidable(x, y);
	}

	private bool IsHorizontalJumpAvalible(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		for(int d = 0; d < distanceCanJump; d++) {
			x += (int)dir;
			if(!grid.IsInGridBounds(x, y) || grid.IsGridSpaceCollidable(x, y)) return false;
		}
		y -= 1;
		return grid.IsInGridBounds(x, y) && grid.IsGridSpaceCollidable(x, y);
	}

	private bool IsVerticalJumpAvalible(int x, int y, Direction dir) {
		if(grid == null || !grid.IsInGridBounds(x, y)) return false;
		x += (int)dir;
		y += heightCanJump;
		if(!grid.IsInGridBounds(x, y) || grid.IsGridSpaceCollidable(x, y)) return false;
		y -= 1;
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
