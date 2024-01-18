using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{
	[SerializeField] private Transform m_CameraMount;
	[SerializeField] private Transform m_Turret;
	[SerializeField] private Transform m_Barrel;

	private TankSO m_Data;
	private bool m_RotationDirty;
	private Coroutine m_CRAimingTurret;

	private void Awake()
	{
	}

	public void Init(TankSO inData)
	{
		m_Data = inData;
	}

	public void SetRotationDirty()
	{
		
	}

	private IEnumerator C_AimTurret()
	{
        Vector3 projectedVec = Vector3.ProjectOnPlane(m_CameraMount.forward, transform.parent.up);

        Quaternion targetRot = Quaternion.LookRotation(projectedVec, transform.parent.up);

        Debug.DrawLine(transform.position, transform.position + projectedVec * 25f, Color.blue);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot,m_Data.TurretData.TurretTraverseSpeed * Time.deltaTime);
        yield return null;
	}
}
