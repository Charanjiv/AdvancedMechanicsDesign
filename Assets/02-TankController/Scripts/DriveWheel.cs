using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DriveWheel : MonoBehaviour
{
	public event Action<bool> OnGroundedChanged;

	[SerializeField] private Rigidbody m_RB;
	[SerializeField] private TankSO m_Data;
	[SerializeField] private Suspension[] m_SuspensionWheels;
	private int m_NumGroundedWheels;
	private bool m_Grounded;
	public float m_Acceleration;

    private float m_fRequestedDir;
    private bool m_canMove = true;
    public void SetAcceleration(float amount)
	{
		//m_Acceleration = amount;
		//StartCoroutine(StartMotor());
		if(m_fRequestedDir != amount)
		{
			m_fRequestedDir = amount;
			m_canMove=true;
		}
		StartMoving();


	}

	private void StartMoving()
	{
		if(m_canMove == true)
		{
            Debug.Log("Movement");
            //Vector3 moveDirection = transform.forward * m_Acceleration;
            //m_RB.MovePosition(m_RB.position + moveDirection);
            //m_RB.AddForce(transform.forward * m_fRequestedDir * m_Data.EngineData.HorsePower, ForceMode.Impulse);
            m_RB.AddForce(transform.forward * m_Acceleration, ForceMode.Impulse);
        }
	}

	IEnumerator StartMotor() 
	{
		Debug.Log("Movement");
			Vector3 moveDirection = transform.forward * m_Acceleration;
			//m_RB.MovePosition(m_RB.position + moveDirection);
			m_RB.AddForce(moveDirection, ForceMode.Impulse);
			yield return null;
		
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