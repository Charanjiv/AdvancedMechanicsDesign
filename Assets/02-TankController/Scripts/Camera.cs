using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform m_SpringArmTarget; 
	[SerializeField] private Transform m_CameraMount;
	[SerializeField] private Camera m_Camera;

	private float m_CameraDist = 5f;

	[SerializeField] private float m_YawSensitivity;
	[SerializeField] private float m_PitchSensitivity;
	[SerializeField] private float m_ZoomSensitivity;

	[SerializeField] private float m_MaxDist;
	[SerializeField] private float m_MinDist;

	[SerializeField] private float m_CameraProbeSize;
	[SerializeField] private Vector3 m_TargetOffset;
	[SerializeField] private float m_MaxYAngle;

    private void Awake()
    {
		m_MaxDist = m_Camera.fieldOfView;
    }

    public void RotateSpringArm(Vector2 change)
	{
		m_TargetOffset.x += change.x * m_YawSensitivity;
		m_TargetOffset.x = Mathf.Repeat(m_TargetOffset.x, 360);
		m_TargetOffset.y -= change.y * m_PitchSensitivity;
		m_TargetOffset.y = Mathf.Clamp(m_TargetOffset.y, -m_MaxYAngle, m_MaxYAngle);

		RaycastHit hit;

		if(Physics.SphereCast(m_CameraMount.position, m_CameraProbeSize, m_TargetOffset, out hit))
		{
			Debug.Log(hit.collider);
		}
		m_SpringArmTarget.transform.rotation = Quaternion.Euler(m_TargetOffset.y, m_TargetOffset.x, 0);
	}

	public void ChangeCameraDistance(float amount)
	{

	}

	private void LateUpdate()
	{

	}
}