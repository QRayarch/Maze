using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTraps : MonoBehaviour {


	public List<TrapHolder> trapHolders;

	[System.Serializable]
	public class TrapHolder {
		public int numPerLevel;
		public Trap trap;
		public float coolDownTime;

		private float coolDownTimer = 0;

		public void Update() {
			coolDownTimer += Time.deltaTime;
		}

		public void Use() {
			coolDownTimer = -coolDownTime;
		}

		public bool CanPlace {
			get{ return coolDownTimer >= 0; }
		}

		public float CoolDownPercent {
			get{ return -coolDownTimer / (float)coolDownTime; }
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int t = 0; t < trapHolders.Count; t++) {
			trapHolders[t].Update();
		}
	}
}
