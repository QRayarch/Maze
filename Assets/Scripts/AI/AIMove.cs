using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(KeyHolder))]
public class AIMove : Move {

	[Header("Pathfinding Config")]
	public int heightCanJump = 3;
	public int distanceCanJump = 3;
	public int maxSearchDistance = 40;

	public float timeStuckBeforeNewPath = 4;
	private float stuckTimer;

	private PathFinder pathFinder;
	private KeyHolder holder;
	private Transform trans;
	private int posX;
	private int posY;
	private float moveDir = 0;
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

		holder = GetComponent<KeyHolder>();
		holder.OnKeyPickedUp += KeyPickedUp;

		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {

		int oldX = posX;
		int oldY = posY;

		posX = (int)(trans.position.x);
		posY = (int)(trans.position.y);

		if(oldX == posX && oldY == posY) {
			stuckTimer += Time.deltaTime;
		}

		if(path != null) {
			PathFinder.Node currentNode = path.nodes[currentNodeIndex];
			bool shouldChangeDir = true;
			bool isJump = currentNode.isJump;
			
			//Next node is the last
			if(currentNodeIndex + 1 == path.Distance) {
				if(posX == currentNode.posX && posY == currentNode.posY) {
					path = null;
					currentNodeIndex = 0;
				}
			} else {
				PathFinder.Node nextNode = path.nodes[currentNodeIndex + 1];
				isJump |= nextNode.isJump;
				if(posX == currentNode.posX && posY >= currentNode.posY) {
					shouldChangeDir = false;
				}
				//We are at the node position. Find next node
				if(posX == currentNode.posX && posY == currentNode.posY) {
					currentNodeIndex++;
					stuckTimer = 0;
				} else if(posX == currentNode.posX && posY == nextNode.posY) {
					currentNodeIndex++;
					stuckTimer = 0;
				}
			}
			if(shouldChangeDir) {
				moveDir = currentNode.posX - posX;
				//Smooth the apprach
				float dist = Vector3.Distance(trans.position, new Vector3(currentNode.posX + 0.5f, currentNode.posY + 0.5f, 0.0f));
				moveDir /= (dist * 1.4f);
			}
			move(moveDir, isJump);
		}

		if(stuckTimer >= timeStuckBeforeNewPath) {
			path = null;
		}

		if(pathFinder != null) {
			if(path == null) {
				Vector3 gotoPos= Vector3.zero;
				Key key = GameObject.FindObjectOfType<Key>();
				if(key != null) {
					gotoPos = key.transform.position;
				} else if(holder.hasKey()) {
					Door door = GameObject.FindObjectOfType<Door>();
					gotoPos = door.transform.position;
				} else {
					bool foundChest = false;
					Chest[] chests = GameObject.FindObjectsOfType<Chest>();
					//Try to find a random unopened chest with a max tries of 100
					for(int c = 0; c < 10; c++) {
						int i = Random.Range(0, chests.Length);
						if(!chests[i].HasOpened) {
							gotoPos = chests[i].transform.position;
							foundChest = true;
							continue;
						}
					}
					if(!foundChest) {
						Vector3 randomPos = Vector3.zero;
						randomPos.x = Random.Range(0, grid.GridWidth);
						randomPos.y = Random.Range(0, grid.GridHeight);

						randomPos.y = pathFinder.FallTillGround((int)randomPos.x, (int)randomPos.y);
						if(!grid.IsGridSpaceCollidable((int)randomPos.x, (int)randomPos.y)) {
							randomPos.x += 0.5f;
							randomPos.y += 0.5f;
							gotoPos = randomPos;
						}
					}
				}
				path = pathFinder.FindPath(trans.position, gotoPos);
				currentNodeIndex = 0;
			}
			/*
			if(Input.GetMouseButton(0)) {
				if(Camera.current != null) {
					path = pathFinder.FindPath(trans.position, Camera.current.ScreenToWorldPoint(Input.mousePosition));
					currentNodeIndex = 0;
				}
			}*/
			pathFinder.DebugDrawPath(path);
		}
	}

	private void KeyPickedUp() {
		path = null;
	}
}
