using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineSampler : MonoBehaviour
{
    private SplineContainer m_splineContainer;

    [SerializeField] private int m_splineIndex;
    [SerializeField][Range(0f, 1f)] private float m_time;

    float3 position;
    float3 tangent;
    float3 upVector;
    private Vector3 forward;
    private Vector3 p1;
    private Vector3 p2;
    [SerializeField] private Vector3 m_width;

    private void Update()
    {
        m_splineContainer.Evaluate(m_splineIndex, m_time, out position, out tangent, out upVector);

        //tangent is the forward direction of travel along the spline to the next point;
        SampleSplineWidth();

    }
    public void SampleSplineWidth()
    {
        float3 right = Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right * m_width);
        p2 = position + (-right * m_width);
        
    }

    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.SphereHandleCap(0, position, Quaternion.identity, 1f, EventType.Repaint);
    }





}
