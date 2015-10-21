using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public List<Vector3> spawnPoints=new List<Vector3>();
	public List<int> enemyTypes=new List<int>();
	public GameObject seeker;
	public GameObject artificer;

	float stepTimer;
	float stepTime=10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		stepTimer += Time.deltaTime;
		if(stepTimer >= stepTime) {
			spawnEnemy();
		}
	}

	void spawnEnemy(){
		stepTimer = 0;

		int enemytype = enemyTypes[0];
		enemyTypes.RemoveAt (0);

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
