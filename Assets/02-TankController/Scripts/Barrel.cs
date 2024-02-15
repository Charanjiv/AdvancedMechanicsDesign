using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	[SerializeField] private TankSO m_Data;
	[SerializeField] private Shell m_ShellPrefab;
	[SerializeField] private ShellSO[] m_AmmoTypes;
	[SerializeField] private int[] m_AmmoCounts;
	private int m_SelectedShell;

	private float m_CurrentDispersion;
	Coroutine m_CRReloading;

	public void Init(TankSO inData)
	{
		
	}

	public void Fire()
	{
		//Shell shell = Instantiate(m_ShellPrefab, transform.position, transform.rotation);
		//shell.transform.parent = null;
		//shell.Fire();
		//m_CRReloading = StartCoroutine(C_Reloding());
    }

    private IEnumerator C_Reloding()
    {
		yield return null;
    }
}






