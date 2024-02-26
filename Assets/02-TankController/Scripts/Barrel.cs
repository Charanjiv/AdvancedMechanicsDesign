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
    [SerializeField] private Transform bulletSpawn;
    private int m_SelectedShell;
	private float m_CurrentDispersion;
	Coroutine m_CRReloading;
	private bool m_Loaded;

	private float m_CurrentTimer;

	public void Init(TankSO inData)
	{
		m_Data = inData;
		m_Loaded = true;

	}

	public void Fire()
	{
		//if (!m_Loaded) return;

		
		//	Shell shell = Instantiate(m_ShellPrefab, bulletSpawn.position, Quaternion.identity);
		//	shell.transform.parent = null;
		//	shell.Init(m_AmmoTypes[m_SelectedShell]);
		//	shell.Fire();
		//	m_AmmoCounts[m_SelectedShell]--;
		//	m_Loaded = false;
		//	m_CRReloading = StartCoroutine(C_Reloding());

		
	}

    public IEnumerator C_Reloding()
    {
		while (!m_Loaded)
		{
			m_CurrentTimer += Time.deltaTime;
			if (m_CurrentTimer >= m_Data.BarrelData.ReloadTime)
			{
				m_CurrentTimer -= m_CurrentTimer;
				m_Loaded = true;

			}
			yield return null;
		}
    }
}






