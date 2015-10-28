
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	
	public List<Vector3> spawnPoints=new List<Vector3>();
	public List<int> enemyTypes=new List<int>();
	public GameObject seeker;
	public GameObject artificer;
	public float firstSpawnDelay=10;
	public float numFirstSpawn;

	private int index = 0;
	bool firstSpawn;

	float stepTimer;
	public float spawnDelay=10;

	// Use this for initialization
	void Start () {
		stepTimer -= firstSpawnDelay;
		firstSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
		stepTimer += Time.deltaTime;
		if(stepTimer >= spawnDelay) {
			if(firstSpawn){
				for(int i=0; i<numFirstSpawn;i++){
					spawnEnemy();
				}
				firstSpawn=false;
			}
			else {
				spawnEnemy ();
			}
			stepTimer = 0;
		}
	}

	void spawnEnemy(){
		if(index < 0 || index >= enemyTypes.Count) return;

		int enemytype = enemyTypes[index];

		Vector3 spawnPoint = spawnPoints [index];

		index++;

		//finish stuff
		if(enemytype==0 && seeker != null){
			Instantiate (seeker, spawnPoint, Quaternion.identity);
		}
		if(enemytype==1){
			//Instantiate (artificer, spawnPoint, Quaternion.identity);
		}
		/*if(enemyType==0){
			Instantiate (seeker, spawnPoint, Quaternion.identity);
		}*/
	}
}
