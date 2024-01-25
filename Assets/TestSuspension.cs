using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSuspension : MonoBehaviour
{
    private Rigidbody m_rb;
    [SerializeField] private float springLength;
    [SerializeField] private float stiffness;
    [SerializeField] private float damping;

    // Start is called before the first frame update
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.down;

        Vector3 localDir = transform.TransformDirection(direction);
        var rb = GetComponent<Rigidbody>();

        Vector3 worldvel = rb.GetPointVelocity(transform.position);

        Vector3 springVec = transform.position - transform.parent.position;

        float suspensionOffset = springLength - Vector3.Dot(springVec, localDir);

        float suspensionVelocity = Vector3.Dot(localDir, worldvel);

        float suspensionForce = (suspensionOffset * stiffness) - (suspensionVelocity * damping);

        rb.AddForce(localDir * (suspensionForce / rb.mass));
    }
}
