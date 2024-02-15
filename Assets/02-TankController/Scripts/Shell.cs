using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private ShellSO m_Data;
    private Rigidbody m_RB;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("hit " + collision.gameObject.name);
            Destroy(gameObject); 
        }
    }
//    public void Fire()
//    {
//        m_RB.AddForce(transform.forward * m_Data.Velocity, ForceMode.Impulse);
//    }
}
