using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Grid))]
[CanEditMultipleObjects]
public class GridEditor : Editor {

	private GUIStyle style = new GUIStyle();

	private ReorderableList list;

	private void OnEnable() {
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("brushes")
		                           , false, true, true, true);
		list.drawElementCallback += DrawBrush;
		list.onSelectCallback += SelectElemet;
	}

	private void DrawBrush(Rect rect, int index, bool isActive, bool isFocused) {
		SerializedProperty brush = list.serializedProperty.GetArrayElementAtIndex(index);
		EditorGUI.PropertyField(rect, brush.FindPropertyRelative("sprite"));
		//style.normal.background = brush.FindPropertyRelative("sprite");
	}

	private void SelectElemet(ReorderableList l) {
		Grid grid = target as Grid;
		grid.selectedBrushIndex = l.index;
		Debug.Log(grid.selectedBrushIndex);
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();

		//Change the UI
		if(GUI.changed) {
			EditorUtility.SetDirty(target);
		}
	}

	private void AddSpaceAtMouse(Event e, Grid grid) {
		Vector2 pos = e.mousePosition;
		//Find mouse relative to scene view
		pos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - pos.y;
		pos = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(pos);

		//Fix cordinates to grid
		pos.x = Mathf.Min(grid.GridWidth - 1,  Mathf.Max(0, (int)(pos.x)));
		pos.y = Mathf.Min(grid.GridHeight - 1,  Mathf.Max(0, (int)(pos.y)));

		//AddTile
		grid.AddGridSpace((int)pos.x, (int)pos.y);
	}

	void OnSceneGUI() {
		Event e = Event.current;
		Grid grid = target as Grid;

		switch(e.type) {
			case EventType.MouseUp :
				if(e.button == 0) {
					AddSpaceAtMouse(e, grid);
				}
				break;
			case EventType.MouseDrag :
				if(e.button == 0) {
					AddSpaceAtMouse(e, grid);
				}
				break;
		}
	}
}
