﻿using UnityEngine;
using System.Collections;

public class AIMove : Move {

	[Header("AI Config")]
	[Range(0.0f, 1.0f)]
	public float jumpChance = 0.01f;
	public float turnChance = 0.08f;

	/*[Header("Weights")]
	public float seekKey = 1;
	public float seekMonster = 1;
	public float seekTrap = 1;*/

	private float seekDoor = 0;

	private PathFinder pathFinder;
	private Transform trans;
	private int posX;
	private int posY;

	//Temp
	PathFinder.Path p;
	float dir = 1;
	bool jumping=false;

	// Use this for initialization
	void Start () {
		base.Start();

		pathFinder = GetComponent<PathFinder>();
		if(pathFinder != null) {
			pathFinder.Grid = grid;
			pathFinder.DistanceCanJump = 3;
			pathFinder.HeightCanJump = 3;
			pathFinder.MaxDistance = 30;
		}
		trans = transform;

		dir = Random.Range(0, 2) == 0 ? 1: -1;
	}
	
	// Update is called once per frame
	void Update () {

		posX = (int)(trans.position.x - 0.5f);
		posY = (int)(trans.position.y - 0.5f);

		move(dir,jumping);

		float choice = Random.Range(0.0f, 1.0f);
		if(choice <= jumpChance) {
			jumping=true;
		} else if (choice - jumpChance <= turnChance) {
			dir *= -1;
		}

		//Turn around if there is a wall
		if(grid.IsGridSpaceCollidable(posX + (int)dir, posY)) {
			dir *= -1;
		}
		if(pathFinder != null) {
			if(Input.GetMouseButton(0)) {
				if(Camera.current != null) {
					p = pathFinder.FindPath(trans.position, Camera.current.ScreenToWorldPoint(Input.mousePosition));
				}
				//p = pathFinder.FindPath(trans.position, Camera.current.ScreenToWorldPoint(Input.mousePosition));

			}
			pathFinder.DebugDrawPath(p);
		}
	}
}
