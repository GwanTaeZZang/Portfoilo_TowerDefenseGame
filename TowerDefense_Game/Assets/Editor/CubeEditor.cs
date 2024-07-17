using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor
{
    private Cube cube;

    private void OnEnable()
    {
        cube = target as Cube;    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        cube.value = EditorGUILayout.IntField("인트", cube.value, GUILayout.ExpandWidth(false));
        cube.value = EditorGUILayout.IntField(cube.value, GUILayout.Width(100));
        cube.value = EditorGUILayout.IntField(cube.value, GUILayout.Width(100), GUILayout.Height(100));

        GUILayout.Box(Resources.Load<Texture2D>("MapSprite/MoveAbleTile"), GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.Box("박스", GUILayout.ExpandWidth(true), GUILayout.MinWidth(200), GUILayout.MaxWidth(400));
        GUILayout.Box("박스", GUILayout.Width(100), GUILayout.Height(100));

        if(GUILayout.Button("Button1", GUILayout.MinWidth(300), GUILayout.MaxWidth(500)))
        {
            Debug.Log("Button1 Click");
        }
        if (GUILayout.RepeatButton("Button2", GUILayout.MinWidth(300), GUILayout.MaxWidth(500)))
        {
            Debug.Log("Button2 Click");
        }
        if (EditorGUILayout.DropdownButton(new GUIContent("DropDownButton"), FocusType.Keyboard))
        {
            Debug.Log("DropDownButton Click");
        }

    }
}
