using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFocusOnGrid : MonoBehaviour {

	public Grid grid;

	private Transform trans;
	private Camera cam;

	// Use this for initialization
	void Start () {
		trans = transform;
		cam = GetComponent<Camera>();
		//Focus();
	}
	
	// Update is called once per frame
	void Update () {
		Focus();
	}

	
	public void Focus() {
		Transform gridT = grid.transform;
		Vector3 pos = trans.position;
		pos.x = gridT.position.x + grid.GridWidth / 2 + 0.5f;
		pos.y = gridT.position.y + grid.GridHeight / 2 + 0.5f;
		trans.position = pos;

		cam.orthographicSize = grid.GridHeight / 2.0f;
	}
}
