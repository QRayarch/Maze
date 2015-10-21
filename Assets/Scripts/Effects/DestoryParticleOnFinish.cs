using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class DestoryParticleOnFinish : MonoBehaviour {

	private ParticleSystem partSystem;

	// Use this for initialization
	void Start () {
		partSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!partSystem.IsAlive()) {
			Destroy(gameObject);
			Debug.Log("DEAD");
		}
	}
}
