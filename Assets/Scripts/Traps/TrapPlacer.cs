using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapPlacer : MonoBehaviour {

	public string inputButton = "PlaceTrap";
	[Header("Aperance")]
	public Material placedMat;
	public Material placingMat;
	public Color canPlaceColor;
	public Color canNotPlaceColor;

	private Transform trans;
	private Grid grid;
	private LevelTraps levelTraps;
	private PlacingTrap trapPlacing;
	private bool isPlacingTrap = false;
	private bool couldPlace = false;

	private struct PlacingTrap {
		public int trapIndexPlacing;
		public Trap trap;
		public Transform transform;
		public SpriteRenderer[] renders;
	}

	// Use this for initialization
	void Start () {
		trans = transform;

		levelTraps = GameObject.FindObjectOfType<LevelTraps>();
		if(levelTraps == null) {
			Debug.LogError("--Can't Find LevelTraps--" +
				"//Trap placer can't find a LevelTraps in the scene. Removing Trap placer.");
			Destroy(this);
		}

		grid = GameObject.FindObjectOfType<Grid>();
		if(grid == null) {
			Debug.LogError("--Can't Find Grid--" +
				"//Trap placer can't find a Grid in the scene. Placing will not work properly");
		}
	}

	// Update is called once per frame
	void Update () {
		//Check for new Traps
		bool didAction = false;
		for(int t = 0; t < levelTraps.trapHolders.Count && !didAction; t++) {
			if(Input.GetButtonDown("Trap " + (t + 1).ToString()) && levelTraps.trapHolders[t].numPerLevel >= 1) {
				//Cancel trap placment if we press the same key again
				if(isPlacingTrap && trapPlacing.trapIndexPlacing == t) { 
					isPlacingTrap = false;
					didAction = true;

					Destroy(trapPlacing.transform.gameObject);
				} else {
					isPlacingTrap = true;
					didAction = true;

					trapPlacing = new PlacingTrap();
					trapPlacing.trapIndexPlacing = t;

					trapPlacing.trap = Instantiate(levelTraps.trapHolders[t].trap);

					trapPlacing.transform = trapPlacing.trap.transform;
					trapPlacing.transform.SetParent(trans, false);
					Vector3 posAdd = Vector3.zero;
					posAdd.x = Mathf.Sign(trans.localScale.x);//Scale x denotes direction
					trapPlacing.transform.position += posAdd;

					GameObject trapGameO = trapPlacing.transform.gameObject;
					trapPlacing.renders = trapGameO.GetComponentsInChildren<SpriteRenderer>();

					SetRendersMaterial(placingMat);
					SetRenderMaterialsColor(canPlaceColor);
				}
			}
		}

		//Check trap placment
		bool canPlace = false;
		if(isPlacingTrap && grid != null) {
			//Get grid pos
			int x = (int)(trans.position.x - 0.5f);
			int y = (int)(trans.position.y - 0.5f);

			//Add one in the direction we are facing
			x += (int)Mathf.Sign(trans.localScale.x);

			if(grid.IsInGridBounds(x, y)) {
				//Can't hit the square we want to place in
				if(!grid.IsGridSpaceCollidable(x, y)) {
					y-= 1;
					if(grid.IsInGridBounds(x, y)) {
						//There is a ground benethe the tile
						if(grid.IsGridSpaceCollidable(x, y)) {
							canPlace = true;
						}
					}
				}
			}

			if(canPlace && !couldPlace) {
				SetRenderMaterialsColor(canPlaceColor);
			} else if(!canPlace && couldPlace) {
				SetRenderMaterialsColor(canNotPlaceColor);
			}

		   couldPlace = canPlace;
		}

		//Check to see if we are placed the trap.
		if(canPlace) {
			if(Input.GetButton(inputButton)) {
				trapPlacing.transform.SetParent(null, true);
				Vector3 scale = trapPlacing.transform.localScale;
				scale.x = Mathf.Abs(scale.x);
				trapPlacing.transform.localScale = scale;
				trapPlacing.trap.Place();

				SetRendersMaterial(placedMat);
				SetRenderMaterialsColor(Color.white);

				LevelTraps.TrapHolder trapHolder = levelTraps.trapHolders[trapPlacing.trapIndexPlacing];
				trapHolder.numPerLevel--;
				levelTraps.trapHolders[trapPlacing.trapIndexPlacing] = trapHolder;
				isPlacingTrap = false;
			}
		}
	}

	private void SetRendersMaterial(Material mat) {
		for(int r = 0; r < trapPlacing.renders.Length; r++) {
			trapPlacing.renders[r].material = mat;
		}
	}

	private void SetRenderMaterialsColor(Color c) {
		for(int r = 0; r < trapPlacing.renders.Length; r++) {
			trapPlacing.renders[r].color = c;
		}
	}
}
