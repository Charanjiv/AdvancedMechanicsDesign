using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class SplineSampler : MonoBehaviour
{
    [SerializeField]
    SplineContainer m_splineContainer;
    [SerializeField]
    private int m_splineIndex;
    [SerializeField]
    [Range (0f, 1f)]
    private float m_time;
    [SerializeField]
    private float m_width;

    float3 m_position;
    float3 m_forward;
    float3 m_upVector;

    Vector3 p1;
    Vector3 p2;

    public SplineContainer Container => m_splineContainer;
    public int NumSplines => m_splineContainer.Splines.Count;

    private void Awake()
    {
        m_splineContainer = gameObject.GetComponent<SplineContainer>();
    }


    private void Update()
    {
        m_splineContainer.Evaluate(m_splineIndex, m_time, out m_position, out m_forward, out m_upVector);

       float3 right = Vector3.Cross(m_forward, m_upVector).normalized;
       p1 = m_position + (right * m_width);
       p2 = m_position + (-right * m_width);

    }

    //public void SampleSplineWidth( float t, float width, out Vector3 p1, out Vector3 p2)
    //{
    //    m_splineContainer.Evaluate( t, out float3 position, out float3 forward, out float3 upVector);

    //    float3 right = Vector3.Cross(forward, upVector).normalized;
    //    p1 = position + (right * width);
    //    p2 = position + (-right * width);
    //}
    public void SampleSplineWidth(int splineIndex, float t, float width, out Vector3 p1, out Vector3 p2)
    {
        m_splineContainer.Evaluate(splineIndex, t, out float3 position, out float3 forward, out float3 upVector);

        float3 right = Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right * width);
        p2 = position + (-right * width);
    }



    private void OnDrawGizmos()
    {

        Handles.matrix = transform.localToWorldMatrix;
        Handles.SphereHandleCap(0, p1, Quaternion.identity, 1f, EventType.Repaint);
        Handles.DrawDottedLine(p1, p2, .5f);
        Handles.SphereHandleCap(0, p2, Quaternion.identity, 1f, EventType.Repaint);

    }
}
//#if UNITY_EDITOR

//[CustomEditor(typeof(SplineSampler))]
//public class SplineSamplerEditor : Editor
//{
//    protected virtual void OnSceneGUI()
//    {
//        SplineSampler procTarget = (SplineSampler)target;

//        EditorGUI.BeginChangeCheck();

//        Vector3 newOffset = Handles.DoPositionHandle(procTarget.transform.position,
//                                                     procTarget.transform.rotation);

//        if (EditorGUI.EndChangeCheck())
//        {
//            Undo.RecordObject(procTarget, "Procedural Values Changed!");
//            procTarget.transform.position = newOffset - procTarget.transform.position;

//        }
//    }
//}

//#endif
