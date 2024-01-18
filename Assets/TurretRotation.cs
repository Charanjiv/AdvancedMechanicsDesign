using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] private Transform m_camera;
    [SerializeField] private float m_rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 projectedVec = Vector3.ProjectOnPlane(m_camera.forward, transform.parent.up);

        Quaternion targetRot = Quaternion.LookRotation(projectedVec, transform.parent.up);

        Debug.DrawLine(transform.position, transform.position + projectedVec * 25f, Color.blue);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, m_rotSpeed * Time.deltaTime);
    }
}
