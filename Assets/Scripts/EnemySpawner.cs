﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	// Use this for initialization
	void Start () {
		Instantiate (enemy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}