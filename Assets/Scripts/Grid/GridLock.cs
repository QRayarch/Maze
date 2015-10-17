#if (UNITY_EDITOR)
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridLock : MonoBehaviour {
	private Transform trans;

	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!Application.isPlaying) {
			SnapPosition();
		}
	}

	private void SnapPosition() {
		Vector3 pos = trans.position;
		pos.x = Mathf.RoundToInt(pos.x - 0.5f) + 0.5f;
		pos.y = Mathf.RoundToInt(pos.y - 0.5f) + 0.5f;
		trans.position = pos;
	}
}
#endif