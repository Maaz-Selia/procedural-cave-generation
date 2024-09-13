using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshRenderer : MonoBehaviour
{
    public bool generateMesh = true;
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    int[] trianglesbw;

    public int xWidth = 20;
    public int zWidth = 20;

    // Start is called before the first frame update
    void Start()
    {
        if (generateMesh == true)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            CreateShape();
            UpdateMesh();
        }
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xWidth + 1) * (zWidth + 1)];
        for (int i = 0, z = 0; z <= zWidth; z++)
        {
            for (int x = 0; x <= xWidth; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xWidth * zWidth * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zWidth; z++)
        {
            for (int x = 0; x < xWidth; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xWidth + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xWidth + 1;
                triangles[tris + 5] = vert + xWidth + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();   mesh.vertices = vertices;   mesh.triangles = triangles;
    }
}
