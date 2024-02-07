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

	public void Init(SuspensionSO inData)
	{
		m_RB = GetComponent<Rigidbody>();
		m_Data = inData;
	}

	public bool GetGrounded()
	{

		return m_Grounded;
		//if (Physics.Raycast(m_Wheel.position, -m_Wheel.up, m_Data.WheelDiameter))
		//{
		//	return m_Grounded = true;

		//}
		//else
		//{
		//	return false;
		//}

	}

	private void FixedUpdate()
	{

		RaycastHit hit;
		bool isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, m_SpringSize, m_Data.SuspensionLayermask);

        if (isGrounded!=m_Grounded)
        {
			m_Grounded = isGrounded;
			OnGroundedChanged.Invoke(m_Grounded);
        }

		Vector3 localDir = transform.TransformDirection(Vector3.up);
		Vector3 worldDir = m_RB.GetPointVelocity(transform.TransformPoint(localDir));
		float susOffset = m_SpringSize - hit.distance;
		float susVel = Vector3.Dot(localDir, worldDir);
		float susForce = (susOffset * m_Data.SuspensionStrength) - (susVel * m_Data.SuspensionDamper);
		m_RB.AddForceAtPosition(localDir * susForce, transform.position, ForceMode.Acceleration);


        //if(GetGrounded())
        //{
        //          Vector3 direction = Vector3.down;

        //          Vector3 localDir = transform.TransformDirection(direction);


        //          Vector3 worldvel = m_RB.GetPointVelocity(transform.position);

        //          Vector3 springVec = transform.position - transform.parent.position;

        //          float suspensionOffset = m_Data.WheelDiameter - Vector3.Dot(springVec, localDir);

        //          float suspensionVelocity = Vector3.Dot(localDir, worldvel);

        //          float suspensionForce = (suspensionOffset * m_Data.SuspensionStrength) - (suspensionVelocity * m_Data.SuspensionDamper);

        //          m_RB.AddForce(localDir * (suspensionForce / m_RB.mass));
        //      }

    }
}
