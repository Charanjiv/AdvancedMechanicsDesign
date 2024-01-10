using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour
{
    private MeshFilter m_Filter;
    private Mesh m_Mesh;
    private List<Vector3> m_Verticies;
    private List<int> m_Tris;
    private List<Vector3> m_Normals;

    private void Awake()
    {
        m_Filter = GetComponent<MeshFilter>();
        m_Mesh = new Mesh { name = "Procedural Cube" };
        m_Filter.mesh = m_Mesh;
    }
    void Start()
    {
        CreateCube();
    }

    private void CreateCube()
    {
        m_Verticies = new List<Vector3>();
        m_Tris = new List<int>();
        m_Normals = new List<Vector3>();

        m_Verticies.Add(new Vector3(0f, 0f, 0f));
        m_Verticies.Add(new Vector3(1f, 0f, 0f));
        m_Verticies.Add(new Vector3(1f, 1f, 0f));
        m_Verticies.Add(new Vector3(0, 1f, 0f));
        m_Verticies.Add(new Vector3(0f, 1f, 1f));
        m_Verticies.Add(new Vector3(1f, 1f, 1f));
        m_Verticies.Add(new Vector3(1f, 0f, 1f));
        m_Verticies.Add(new Vector3(0f, 0f, 1f));

        m_Tris.Add(0);
        m_Tris.Add(2);
        m_Tris.Add(1);

        m_Tris.Add(0);
        m_Tris.Add(3);
        m_Tris.Add(2);

        m_Tris.Add(2);
        m_Tris.Add(3);
        m_Tris.Add(4);

        m_Tris.Add(2);
        m_Tris.Add(4);
        m_Tris.Add(5);

        m_Tris.Add(1);
        m_Tris.Add(2);
        m_Tris.Add(5);

        m_Tris.Add(1);
        m_Tris.Add(5);
        m_Tris.Add(6);

        m_Tris.Add(0);
        m_Tris.Add(7);
        m_Tris.Add(4);

        m_Tris.Add(0);
        m_Tris.Add(4);
        m_Tris.Add(3);

        m_Tris.Add(5);
        m_Tris.Add(4);
        m_Tris.Add(7);

        m_Tris.Add(5);
        m_Tris.Add(7);
        m_Tris.Add(6);

        m_Tris.Add(0);
        m_Tris.Add(6);
        m_Tris.Add(7);

        m_Tris.Add(0);
        m_Tris.Add(1);
        m_Tris.Add(6);

        m_Mesh.Clear();
        m_Mesh.SetVertices(m_Verticies);
        m_Mesh.SetTriangles(m_Tris, 0);
        m_Mesh.RecalculateNormals();

    }
}
