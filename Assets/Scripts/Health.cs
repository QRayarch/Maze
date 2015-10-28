using UnityEngine;
using System.Collections;

public delegate void OnDie();

public class Health : MonoBehaviour {

	public int maxHealth;

	public GameObject onHitSpawn;

	private event OnDie onDeath;
	private int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int d) {
		if(d < 0) {
			Debug.LogWarning("Taking negative damage. Actually giving health.");
		}

		bool wasDead = IsDead;
		health = Mathf.Clamp(health - d, 0, maxHealth);

		if(!IsDead && onHitSpawn != null) {
			Instantiate(onHitSpawn, transform.position, Quaternion.identity);
		}

		if(IsDead && !wasDead) {
			if(onDeath != null) {
				onDeath.Invoke();
			}
		}
	}

	public bool IsDead {
		get { return health <= 0; }
	}

	public OnDie OnDeath {
		get{ return onDeath; }
		set{ onDeath = value; }
	}
}
