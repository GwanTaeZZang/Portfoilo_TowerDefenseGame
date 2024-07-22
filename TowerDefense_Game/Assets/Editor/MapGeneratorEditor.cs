using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    private MapGenerator mapGenerator;
    private int tileNum;
    //private GameObject mapParent;

    private void OnEnable()
    {
        mapGenerator = target as MapGenerator;
    }

    public override void OnInspectorGUI()
    {

        EditorGUILayout.PrefixLabel("Map Name");
        mapGenerator.mapName = EditorGUILayout.TextField("Map Name", mapGenerator.mapName);

        EditorGUILayout.PrefixLabel("Map Size");
        mapGenerator.width = EditorGUILayout.IntField("Width", mapGenerator.width);
        mapGenerator.height = EditorGUILayout.IntField("Height", mapGenerator.height);
        if (GUILayout.Button("Create Map", GUILayout.ExpandWidth(true), GUILayout.MinWidth(200), GUILayout.MaxWidth(400)))
        {
            mapGenerator.CreateMap(mapGenerator.width, mapGenerator.height);
            mapGenerator.isCreateMap = true;
        }

        if (mapGenerator.isCreateMap)
        {

            //Debug.Log(mapGenerator.isSetEndWayTile);
            //Debug.Log(mapGenerator.isSetWayTile);
            //Debug.Log(mapGenerator.isSetTowerBuileTile);


            if (!mapGenerator.isSetEndWayTile)
            {
                EditorGUILayout.LabelField("Set EndWay Tile");
            }
            else if (!mapGenerator.isSetWayTile)
            {
                EditorGUILayout.LabelField("Set Way Tile");

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add Way", GUILayout.ExpandWidth(true), GUILayout.MinWidth(300), GUILayout.MaxWidth(600)))
                {
                    mapGenerator.AddWay();
                }
                if (GUILayout.Button("Delete Way", GUILayout.ExpandWidth(true), GUILayout.MinWidth(300), GUILayout.MaxWidth(600)))
                {
                    mapGenerator.DeleteWay();
                }
                EditorGUILayout.EndHorizontal();

                int count = mapGenerator.wayListStack.Count;
                for(int i = 0; i < count; i++)
                {
                    EditorGUILayout.LabelField("Way_" + i);
                }


            }
            else if (!mapGenerator.isSetTowerBuileTile)
            {
                EditorGUILayout.LabelField("Set Tower Build Tile");
            }

            int width = mapGenerator.width;
            int height = mapGenerator.height;

            for (int i = 0; i < height; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < width; j++)
                {

                    tileNum = ((height - (i + 1)) * width) + j;

                    //Debug.Log(mapGenerator.tileList.Count);
                    //Debug.Log(i);
                    //Debug.Log(j);
                    //Debug.Log(tileNum);


                    if (mapGenerator.tileList[tileNum].isEndWay)
                    {
                        GUI.backgroundColor = Color.red;
                    }
                    else if(mapGenerator.tileList[tileNum].moveAble)
                    {
                        GUI.backgroundColor = Color.green;
                    }
                    else if (mapGenerator.tileList[tileNum].towerBuledAble)
                    {
                        GUI.backgroundColor = Color.yellow;
                    }
                    else
                    {
                        GUI.backgroundColor = Color.white;
                    }

                    if (GUILayout.Button(tileNum.ToString(), GUILayout.Width(30), GUILayout.Height(30)))
                    {
                        mapGenerator.SetTile(tileNum);
                    }
                }
                EditorGUILayout.EndHorizontal();

            }

            if(mapGenerator.isSetEndWayTile && !mapGenerator.isSetWayTile)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Prev", GUILayout.ExpandWidth(true), GUILayout.MinWidth(300), GUILayout.MaxWidth(600)))
                {

                }
                if (GUILayout.Button("Next", GUILayout.ExpandWidth(true), GUILayout.MinWidth(300), GUILayout.MaxWidth(600)))
                {

                }
                EditorGUILayout.EndHorizontal();

            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Conform", GUILayout.ExpandWidth(true), GUILayout.MinWidth(300), GUILayout.MaxWidth(600)))
            {
                if (!mapGenerator.isSetEndWayTile)
                {
                    mapGenerator.isSetEndWayTile = true;
                }
                else if (!mapGenerator.isSetWayTile)
                {
                    mapGenerator.isSetWayTile = true;
                }
                else if (!mapGenerator.isSetTowerBuileTile)
                {
                    mapGenerator.isSetTowerBuileTile = true;
                }
            }
            if (GUILayout.Button("Cancel", GUILayout.ExpandWidth(true), GUILayout.MinWidth(300), GUILayout.MaxWidth(600)))
            {
                if (!mapGenerator.isSetEndWayTile)
                {
                    mapGenerator.CancelEndWayTile();
                }
                else if (!mapGenerator.isSetWayTile)
                {
                    mapGenerator.CancelWayTile();
                    mapGenerator.isSetEndWayTile = false;
                }
                else if (!mapGenerator.isSetTowerBuileTile)
                {
                    mapGenerator.CancelTowerBuildTile();
                    mapGenerator.isSetWayTile = false;
                }
            }
            EditorGUILayout.EndHorizontal();

        }

        //EditorGUILayout.BeginHorizontal();


        if (GUILayout.Button("Delete Map", GUILayout.ExpandWidth(true), GUILayout.MinWidth(200), GUILayout.MaxWidth(400)))
        {
            //GameObject map = GameObject.Find("Map");
            //DestroyImmediate(map);
            //mapGenerator.isCreateMap = false;

            mapGenerator.DestroyMap();
            tileNum = 0;

        }


        if (GUILayout.Button("To Json Map Data", GUILayout.ExpandWidth(true), GUILayout.MinWidth(200), GUILayout.MaxWidth(400)))
        {
            mapGenerator.CreateJsonMapData();
        }


    }
}
