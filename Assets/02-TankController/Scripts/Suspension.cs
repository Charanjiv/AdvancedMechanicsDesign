using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
	public event Action<bool> OnGroundedChanged; 

	[SerializeField] private Transform m_Wheel;
	[SerializeField] private Rigidbody m_RB;

	private SuspensionSO m_Data;
	private float m_SpringSize;
	private bool m_Grounded;

	private bool isGrounded;
	RaycastHit hit;


    public void Init(SuspensionSO inData)
	{
		
		m_Data = inData;
		isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, m_SpringSize, m_Data.SuspensionLayermask);
	}

	public bool GetGrounded()
	{

		return m_Grounded;

	}

	private void FixedUpdate()
	{
		GetGrounded();

		if (isGrounded != m_Grounded)
		{
			m_Grounded = isGrounded;
			OnGroundedChanged.Invoke(m_Grounded);
		}

		if (m_Grounded)
		{
			//Vector3 localDirection = transform.TransformDirection(Vector3.up);
			//Vector3 worldDirection = m_RB.GetPointVelocity(transform.TransformPoint(localDirection));
			float suspensionOffset = m_SpringSize - hit.distance;
			float suspensionVel = Mathf.Abs(Vector3.Dot(m_RB.GetPointVelocity(transform.position), transform.up));
			float suspensionForce = Mathf.Abs((suspensionOffset * m_Data.SuspensionStrength) - (suspensionVel * m_Data.SuspensionDamper));


			float slipSpeed =Vector3.Dot(m_RB.GetPointVelocity(transform.position), transform.right);
			float steerForce = -slipSpeed;
			float threshold = m_RB.mass * MathF.Sin(35);
			if(slipSpeed > threshold)
			{
				steerForce = threshold * 0.81f;
			}



            m_RB.AddForceAtPosition((transform.up * suspensionForce) + (transform.right * - slipSpeed), transform.position, ForceMode.Acceleration);

		}
	}
}
