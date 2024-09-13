using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveRoom : MonoBehaviour
{

    void Start()
    {
        Mesh myMesh = new Mesh();
        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        gameObject.transform.localScale = new Vector3(30, 30, 1);
        gameObject.GetComponent<MeshFilter>().mesh = myMesh;
    }
}
