using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatePlane : Editor
{
    private const float XLenght = 10f;
    private const float YLength = 10f;
    private const int XVertexCount = 100;
    private const int YVertexCount = 100;

    private const string AssetPath = "Assets/Prefab/";
    private const string MeshPath = "Assets/Models/";

    [MenuItem("GameObject/CreatePlaneGameobject")]
    static void CreatePlaneGameobject()
    {
        Mesh planeMesh = CreateMesh();
        planeMesh.name = string.Format("Mesh{0}x{1}", XVertexCount, YVertexCount);
        AssetDatabase.CreateAsset(planeMesh, MeshPath + planeMesh.name);
        AssetDatabase.SaveAssets();
        CreatePlaneAsset(planeMesh);
    }

    static Mesh CreateMesh()
    {
        Vector3[] vertices = new Vector3[XVertexCount * YVertexCount];
        Vector2[] uv = new Vector2[vertices.Length];

        int i = 0;
        for (int z = 0; z < YVertexCount; z++)
        {
            for (int x = 0; x < XVertexCount; x++, i++)
            {
                vertices[i] = new Vector3(((float)x / (XVertexCount - 1)) * XLenght - XLenght / 2f, 
                                           0, 
                                           ((float)z / (YVertexCount - 1)) * YLength - YLength / 2f);
                uv[i] = new Vector2(((float)x / (XVertexCount - 1)), ((float)z / (YVertexCount - 1)));
            }
        }

        int[] triangles = new int[(XVertexCount - 1) * (YVertexCount - 1) * 6];
        for (int ti = 0, vi = 0, y = 0; y < YVertexCount - 1; y++, vi++)
        {
            for (int x = 0; x < XVertexCount - 1; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = vi + 1;
                triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = vi + XVertexCount;
                triangles[ti + 1] = vi + XVertexCount;
                triangles[ti + 5] = vi + XVertexCount + 1;
            }
        }


        Mesh planeMesh = new Mesh();
        planeMesh.vertices = vertices;
        planeMesh.triangles = triangles;
        planeMesh.RecalculateNormals();
        planeMesh.uv = uv;

        return planeMesh;
    }

    static void CreatePlaneAsset(Mesh planeMesh)
    {
        string objectName = string.Format("Plane{0}x{1}", XVertexCount, YVertexCount);

        GameObject planeObject = new GameObject(objectName, typeof(MeshFilter), typeof(MeshRenderer));
        planeObject.GetComponent<MeshFilter>().mesh = planeMesh;
    }
}
