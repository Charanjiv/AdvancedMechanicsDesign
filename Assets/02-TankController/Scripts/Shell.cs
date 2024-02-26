using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private ShellSO m_Data;
    private Rigidbody m_RB;

    public void Init(ShellSO data)
    {
        m_Data = data;
        m_RB = GetComponent<Rigidbody>();
    }

    public void Fire()
    {
        m_RB.AddForce(transform.forward * m_Data.Velocity, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
