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

	void OnSceneGUI() {
		Event e = Event.current;
		Grid grid = target as Grid;

		switch(e.type) {
			case EventType.MouseUp :
				if(e.button == 0) {
					Vector2 pos = e.mousePosition;
					pos = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(pos);
					grid.AddGridSpaceFromMousePos(pos);
				}
				break;
		}
	}
}
