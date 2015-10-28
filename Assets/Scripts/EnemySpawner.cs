using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	
	public List<Vector3> spawnPoints=new List<Vector3>();
	public List<int> enemyTypes=new List<int>();
	public GameObject seeker;
	public GameObject artificer;
	public float firstSpawnDelay=30;
	public float numFirstSpawn;
	bool firstSpawn;

	float stepTimer=-30;
	public float spawnDelay=10;

	// Use this for initialization
	void Start () {
		firstSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
		stepTimer += Time.deltaTime;
		if(stepTimer >= spawnDelay) {
			if(firstSpawn){
				for(int i=0; i<(numFirstSpawn+1);i++){
					spawnEnemy();
					firstSpawn=false;
				}
			}
			else{
				spawnEnemy ();
			}
		}
	}

	void spawnEnemy(){
		stepTimer = 0;

		int enemytype = enemyTypes[0];
		enemyTypes.RemoveAt (0);

		Vector3 spawnPoint = spawnPoints [0];
		spawnPoints.RemoveAt (0);

		//finish stuff
		if(enemytype==0){
			Instantiate (seeker, spawnPoint, Quaternion.identity);
		}
		if(enemytype==1){
			Instantiate (artificer, spawnPoint, Quaternion.identity);
		}
		/*if(enemyType==0){
			Instantiate (seeker, spawnPoint, Quaternion.identity);
		}*/
	}
}
