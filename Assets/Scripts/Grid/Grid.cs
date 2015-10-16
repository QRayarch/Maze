using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum BrushType {
	NONE,
	NON_COLLIDE,
	COLLIDE,
}

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class Grid : MonoBehaviour {

	//Stuff for brushes
	[Header("Tile Brushes")]
	public Material matForTiles;
	//Hide the things we overriede
	[HideInInspector]
	public List<GridBrush> brushes = new List<GridBrush>();
	[HideInInspector]
	public int selectedBrushIndex;

	[System.Serializable]
	public class GridBrush {
		public Sprite sprite;
		public BrushType type = BrushType.NONE;

		public bool HasSprite {
			get { return sprite != null; }
		}
	}

	//General stuff

	private GameObject[,] spaces;

	private Vector2 oldSize;
	private Transform trans;
	private RectTransform recT;
	private Transform tilesTrans;

	// Use this for initialization
	void Start () {
		selectedBrushIndex = 0;
		trans = transform;
		recT = GetComponent<RectTransform>();
		recT.pivot = Vector2.zero;

		tilesTrans = CheckAddTileLayer();
		ReloadGridSpaces();
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
		}
		oldSize = size;
		
		trans.position = Vector3.zero;
	}

	private void ChangeSize(int newW, int newH) {
		int oldW = GridWidth;
		int oldH = GridHeight;

		//If nothing changed why change things? Just quit
		if(newW == oldW && newH == oldH) return;

		if(spaces != null) {
			GameObject[,] oldSpaces = spaces;//Save old Grid
			for(int x = newW - 1; x < oldW; x++) {
				for(int y = newH - 1; y < oldH; y++) {
					RemoveGridSpace(x, y);
				}
			}
			spaces = new GameObject[newW, newH];//Set up new Grid
			
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
			spaces = new GameObject[newW, newH];//Set up new Grid
		}
	}

	public bool AddGridSpace(int x, int y) {
		if(selectedBrushIndex < 0 || selectedBrushIndex >= brushes.Count) return false;
		return AddGridSpace(x, y, brushes[selectedBrushIndex]);
	}

	public bool AddGridSpace(int x, int y, GridBrush brush) {
		if(!brush.HasSprite && brush.type == BrushType.NONE) {
			RemoveGridSpace(x, y);
			return true;
		}
		if(spaces[x, y] != null) return false;
		GameObject newSpace = new GameObject();
		newSpace.name = BuildPosString(x, y);
		newSpace.isStatic = true;

		if(tilesTrans == null) {
			tilesTrans = CheckAddTileLayer();
		}

		Transform newSpaceT = newSpace.transform;
		trans.position = Vector3.zero;
		newSpaceT.SetParent(tilesTrans, false);
		newSpaceT.position = new Vector3(x + 0.5f, y + 0.5f, 0.0f);
		float width = 1.0f / brush.sprite.bounds.size.x;
		float height = 1.0f / brush.sprite.bounds.size.y;
		newSpaceT.localScale = new Vector3(width, height, 1.0f);

		if(brush.HasSprite) {
			SpriteRenderer spriteRender =  newSpace.AddComponent<SpriteRenderer>();
			if(matForTiles != null) {
				spriteRender.material = matForTiles;
			}
			spriteRender.sprite = brush.sprite;
		}

		spaces[x, y] = newSpace;

		switch(brush.type) {
			case BrushType.COLLIDE :
				newSpace.AddComponent<BoxCollider2D>();
				break;
		}

		return true;
	}

	public void RemoveGridSpace(int x, int y) {
		GameObject deletedSpace = spaces[x, y];
		if(deletedSpace == null) return;
		spaces[x, y] = null;
		DestroyImmediate(deletedSpace);
	}

	public void ReloadGridSpaces() {
		Vector2 size = recT.sizeDelta;
		if(spaces == null) {
			spaces = new GameObject[(int)size.x,(int)size.y];
		}
		tilesTrans = CheckAddTileLayer();
		for(int x = 0; x < GridWidth; x++) {
			for(int y = 0; y < GridHeight; y++) {
				Transform t = tilesTrans.Find(BuildPosString(x, y));
				if(t != null) {
					spaces[x, y] = t.gameObject;
				}
			}
		}
	}

	private string BuildPosString(int x, int y) {
		return "(" + x + ", " + y + ")";
	}

	public Transform CheckAddTileLayer() {
		Transform t = trans.FindChild("Tiles");
		if(t == null) {
			GameObject tiles = new GameObject();
			tiles.isStatic = true;
			tiles.name = "Tiles";
			tiles.transform.SetParent(trans, false);
			return tiles.transform;
		}
		return t;
	}

	public bool IsGridSpaceCollidable(int x, int y) {
		if(!IsInGridBoundsWidth(x) || !IsInGridBoundsHeight(y) || spaces[x, y] == null) return false;
		return spaces[x, y].GetComponent<Collider2D>() != null;
	}

	public bool IsInGridBoundsWidth(int x) {
		return x >= 0 && x < GridWidth;
	}

	
	public bool IsInGridBoundsHeight(int y) {
		return y >= 0 && y < GridHeight;
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
			Gizmos.DrawLine(temp, temp + Vector3.up * GridHeight);
		}
		for(int y = 0; y <= GridHeight; y++) {
			temp = trans.position + Vector3.up * y;
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
