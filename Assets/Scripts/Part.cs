using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Part : MonoBehaviour
{
    // TODO: Create part data and assign here
    private Rigidbody m_rigidbody;

    private Vector3 m_prevVelocity;

    public PartsFactory Owner { get; private set; }
    public bool InGrab { get; private set; }

    public void SetOwner(PartsFactory owner)
    {
        if (Owner == null) Owner = owner;
    }

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (InGrab)
        {
            m_prevVelocity = m_rigidbody.velocity;
        }
    }

    public void StartGrab()
    {
        InGrab = true;
        m_rigidbody.isKinematic = false;
        m_rigidbody.useGravity = false;
    }

    public void EndGrab()
    {
        m_rigidbody.useGravity = true;
        InGrab = false;
    }
}
