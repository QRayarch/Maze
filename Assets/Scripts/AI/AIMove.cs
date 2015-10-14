using UnityEngine;
using System.Collections;

public class AIMove : Move {

	[Header("AI Config")]
	[Range(0.0f, 1.0f)]
	public float jumpChance = 0.01f;

	[Header("Weights")]
	public float seeKey = 1;

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
