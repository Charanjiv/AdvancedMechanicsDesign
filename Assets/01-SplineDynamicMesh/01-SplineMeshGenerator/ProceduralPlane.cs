using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralPlane : MonoBehaviour
{
    private MeshFilter m_Filter;
    private Mesh m_Mesh;
    private List<Vector3> m_Verticies;
    private List<int> m_Tris;
    private List<Vector3> m_Normals;

    private void Awake()
    {
        m_Filter = GetComponent<MeshFilter>();
        m_Mesh = new Mesh { name = "Procedural Mesh" };
        m_Filter.mesh = m_Mesh;
    }

    private void Start()
    {
        m_Verticies = new List<Vector3>();
        m_Tris = new List<int>();
        m_Normals = new List<Vector3>();

        m_Verticies.Add(new Vector3(-0.5f, 0.5f, 0f));
        m_Verticies.Add(new Vector3(0.5f, 0.5f, 0f));
        m_Verticies.Add(new Vector3(0.5f, -0.5f, 0f));
        m_Verticies.Add(new Vector3(-0.5f, -0.5f, 0f));

        m_Normals.Add(new Vector3(-0.5f, -0.5f, -1f));
        m_Normals.Add(new Vector3(-0.5f, -0.5f, -1f));
        m_Normals.Add(new Vector3(-0.5f, -0.5f, -1f));
        m_Normals.Add(new Vector3(-0.5f, -0.5f, -1f));

        m_Tris.Add(0);
        m_Tris.Add(1);
        m_Tris.Add(3);

        m_Tris.Add(1);
        m_Tris.Add(2);
        m_Tris.Add(3);

        m_Mesh.Clear();
        m_Mesh.SetVertices(m_Verticies);
        m_Mesh.SetTriangles(m_Tris, 0);
        m_Mesh.SetNormals(m_Normals);
    }



}
