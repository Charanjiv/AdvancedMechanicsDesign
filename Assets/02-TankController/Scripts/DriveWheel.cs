using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveWheel : MonoBehaviour
{
	public event Action<bool> OnGroundedChanged;

	[SerializeField] private Rigidbody m_RB;
	[SerializeField] private TankSO m_Data;
	[SerializeField] private Suspension[] m_SuspensionWheels;
	private int m_NumGroundedWheels;
	private bool m_Grounded;

	private float m_Acceleration;
	public void SetAcceleration(float amount)
	{
		m_Acceleration = amount;
		Vector3 moveDirection = transform.forward * amount;
		m_RB.MovePosition(m_RB.position + moveDirection);
	}

	public void Init(TankSO inData)
	{
		m_Data = inData;
		
	}

	private void Handle_WheelGroundedChanged(bool newGrounded)
	{
		
	}

	private void FixedUpdate()
	{
		
	}
}