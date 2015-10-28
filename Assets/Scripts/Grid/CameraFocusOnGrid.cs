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
		if(grid.GridWidth > grid.GridHeight) {
			cam.orthographicSize = (grid.GridWidth / 2.0f) / cam.aspect + 1;
		} else {
			cam.orthographicSize = grid.GridHeight / 2.0f + 1;
		}

		float extraWidth = (cam.orthographicSize * cam.aspect) -  (grid.GridWidth / 2);
		Debug.Log(extraWidth);
		pos.x = gridT.position.x + (grid.GridWidth / 2) - extraWidth;
		pos.y = gridT.position.y + grid.GridHeight / 2;

		trans.position = pos;
	}
}
