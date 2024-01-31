using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

[RequireComponent(typeof(SplineSampler))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode()]
public class SplineRoad : MonoBehaviour
{
    private SplineSampler m_splineSampler;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    private List<Vector3> m_vertsP1;
    private List<Vector3> m_vertsP2;

    [SerializeField, Min(5)]
    private int resolution = 10;
    [SerializeField]
    private float m_width;


    private void Awake()
    {
        m_splineSampler = gameObject.GetComponent<SplineSampler>();
        m_meshFilter = gameObject.GetComponent<MeshFilter>();
        m_meshRenderer = gameObject.GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        GetVerts();
        BuildMesh();
    }


    private void GetVerts()
    {
        m_vertsP1 = new List<Vector3>();
        m_vertsP2 = new List<Vector3>();

        float step = 1f / (float)resolution;
        Vector3 p1;
        Vector3 p2;

      
            for (int i = 0; i < resolution; i++)
            {
            float t = step * i;
                m_splineSampler.SampleSplineWidth(t, m_width, out p1, out p2);
                m_vertsP1.Add(p1);
                m_vertsP2.Add(p2);
            }

            m_splineSampler.SampleSplineWidth(1f, m_width, out p1, out p2);
            m_vertsP1.Add(p1);
            m_vertsP2.Add(p2);
        

    }

    private void OnDrawGizmosSelected()
    {

        for (int i = 0; i < m_vertsP1.Count; i++)
        {
            Vector3 p1 = m_vertsP1[i];
            Vector3 p2 = m_vertsP2[i];

            Handles.matrix = transform.localToWorldMatrix;
            Handles.SphereHandleCap(0, p1, Quaternion.identity, .2f, EventType.Repaint);
            Handles.SphereHandleCap(0, p2, Quaternion.identity, .2f, EventType.Repaint);
        }
    }


    private void BuildMesh()
    {
        Mesh m = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int offset = 0;

        int length = m_vertsP2.Count;


        for (int i = 1; i <= length; i++)
        {
            Vector3 p1 = m_vertsP1[i - 1];
            Vector3 p2 = m_vertsP2[i - 1];
            Vector3 p3;
            Vector3 p4;

            if (i == length)
            {
                p3 = m_vertsP1[0];
                p4 = m_vertsP2[0];
            }
            else
            {
                p3 = m_vertsP1[i];
                p4 = m_vertsP2[i];
            }

            offset = 4 * (i - 1);
            int t1 = offset + 0;
            int t2 = offset + 2;
            int t3 = offset + 3;

            int t4 = offset + 3;
            int t5 = offset + 1;
            int t6 = offset + 0;

            verts.AddRange(new List<Vector3> { p1, p2, p3, p4 });
            tris.AddRange(new List<int> { t1, t2, t3, t4, t5, t6 });

        }

        m.SetVertices(verts);
        m.SetTriangles(tris, 0);
        m_meshFilter.mesh = m;


        //private void OnEnable()
        //{
        //    Spline.Changed += OnSplineChanged;
        //    GetVerts();
        //}

        //private void OnDisable()
        //{
        //    Spline.Changed -= OnSplineChanged;
        //}

        //private void OnSplineChanged(Spline arg1, int arg2, SplineModification arg3)
        //{

        //}
    }
}
