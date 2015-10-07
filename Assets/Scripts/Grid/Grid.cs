﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class Grid : MonoBehaviour {

	//Stuff for brushes
	[Header("Tile Brushes")]
	public List<GridBrush> brushes = new List<GridBrush>();
	public int selectedBrushIndex;

	[System.Serializable]
	public class GridBrush {
		public Sprite sprite;

		public bool HasSprite {
			get { return sprite != null; }
		}
	}

	//General stuff

	private GridSpace[,] spaces;

	private Vector2 oldSize;
	private Transform trans;
	private RectTransform recT;

	// Use this for initialization
	void Start () {
		trans = transform;
		recT = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckChangeSize();
	}

	private void CheckChangeSize() {
		Vector2 size = recT.sizeDelta;
		size.x = Mathf.Max(0, Mathf.Round(size.x));
		size.y = Mathf.Max(0, Mathf.Round(size.y));
		recT.sizeDelta = size;
		if(size != oldSize || spaces == null) {
			ChangeSize((int)size.x, (int)size.y);
			Vector3 newPos = trans.position;
			newPos.y = size.y;
			trans.position = newPos;
		}
		oldSize = size;
	}

	private void ChangeSize(int newW, int newH) {
		int oldW = GridWidth;
		int oldH = GridHeight;

		//If nothing changed why change things? Just quit
		if(newW == oldW && newH == oldH) return;

		if(spaces != null) {
			GridSpace[,] oldSpaces = spaces;//Save old Grid
			spaces = new GridSpace[newW, newH];//Set up new Grid
			
			//Copy over new Squares
			for(int x = 0; x < newW; x++) {
				for(int y = 0; y < newH; y++) {
					//If things are getting bigger don't copy
					if(x < oldW && y < oldH) {
						spaces[x, y] = oldSpaces[x, y];
					}
				}
			}
		} else {
			spaces = new GridSpace[newW, newH];//Set up new Grid
		}
	}

	public void AddGridSpaceFromMousePos(Vector2 mousePos) {
	
		Debug.Log(mousePos + "PRE");

		mousePos.x = Mathf.Max(GridWidth - 1, Mathf.Min(0, mousePos.x));
		mousePos.y = Mathf.Max(GridHeight - 1, Mathf.Min(0, mousePos.y));

		Debug.Log(mousePos);

		AddGridSpace((int)mousePos.x, 
		             (int)mousePos.y, 
		             brushes[selectedBrushIndex]);
	}

	public bool AddGridSpace(int x, int y, GridBrush brush) {
		if(!brush.HasSprite) {
			RemoveGridSpace(x, y);
			return true;
		}
		if(spaces[x, y] != null) return false;
		GameObject newSpace = new GameObject();
		newSpace.name = "(" + x + ", " + y + ")";

		Transform newSpaceT = newSpace.transform;
		newSpaceT.SetParent(trans, false);
		Vector3 center = brush.sprite.bounds.center;
		newSpaceT.position = new Vector3(x + 0.5f, GridHeight - y + 0.5f, 0.0f);
		float width = 1.0f / brush.sprite.bounds.size.x;
		float height = 1.0f / brush.sprite.bounds.size.y;
		newSpaceT.localScale = new Vector3(width, height, 1.0f);

		spaces[x, y] = newSpace.AddComponent<GridSpace>();

		SpriteRenderer spriteRender =  newSpace.AddComponent<SpriteRenderer>();
		spriteRender.sprite = brush.sprite;

		return true;
	}

	public void RemoveGridSpace(int x, int y) {
		GridSpace deletedSpace = spaces[x, y];
		if(deletedSpace == null) return;
		spaces[x, y] = null;
		Destroy(deletedSpace.gameObject);
	}

	public int GetBoundedXCortinate(float x) {
		return Mathf.Min(GridWidth - 1,  Mathf.Max(0, (int)Mathf.Round(x)));
	}

	public int GetBoundedYCortinate(float y) {
		return 	Mathf.Min(GridHeight - 1,  Mathf.Max(0, (int)Mathf.Round(y)));
	}

	#region Editor

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		DrawGrid();
	}
	
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		DrawGrid();
	}
	
	private void DrawGrid() {
		Vector3 temp;
		for(int x = 0; x <= GridWidth; x++) {
			temp = trans.position + Vector3.right * x;
			Gizmos.DrawLine(temp, temp + Vector3.down * GridHeight);
		}
		for(int y = 0; y <= GridHeight; y++) {
			temp = trans.position + Vector3.down * y;
			Gizmos.DrawLine(temp + Vector3.right * GridWidth, temp);
		}
	}

	#endregion

	//Properties

	public int GridWidth {
		get {
			if(spaces == null) return - 1;
			return spaces.GetLength(0); 
		}
	}

	public int GridHeight {
		get{ 
			if(spaces == null) return - 1;
			return spaces.GetLength(1); 
		}
	}
}
