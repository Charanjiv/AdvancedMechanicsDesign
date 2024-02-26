using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPIN : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * m_Speed * 360.0f, Space.Self);
    }
}
