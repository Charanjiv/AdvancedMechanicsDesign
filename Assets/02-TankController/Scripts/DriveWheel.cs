using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
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

    
    public bool m_canMove;
    private Coroutine m_CRMove;
    public void SetAcceleration(float amount)
	{


		if (m_canMove == true)
		{
			m_Acceleration = amount;
			m_CRMove = StartCoroutine(C_MoveUpdate());

		}




		if (m_canMove == false)
		{
			StopCoroutine(C_MoveUpdate());
		}

				

        

		


    }

    private IEnumerator C_MoveUpdate()
	{
        m_RB.AddForce(transform.forward * m_Data.EngineData.HorsePower * m_Acceleration, ForceMode.Acceleration);
		Debug.Log("Movement");
		yield return new WaitForFixedUpdate();
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