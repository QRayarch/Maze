using UnityEngine;
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

	private Transform trans;
	private int posX;
	private int posY;

	//Temp
	float dir = 1;

	// Use this for initialization
	void Start () {
		base.Start();

		trans = transform;

		dir = Random.Range(0, 2) == 0 ? 1: -1;
	}
	
	// Update is called once per frame
	void Update () {

		posX = (int)(trans.position.x - 0.5f);
		posY = (int)(trans.position.y - 0.5f);

		move(dir);

		float choice = Random.Range(0.0f, 1.0f);
		if(choice <= jumpChance) {
			base.jump();
		} else if (choice - jumpChance <= turnChance) {
			dir *= -1;
		}

		//Turn around if there is a wall
		if(grid.IsGridSpaceCollidable(posX + (int)dir, posY)) {
			dir *= -1;
		}

	}
}
