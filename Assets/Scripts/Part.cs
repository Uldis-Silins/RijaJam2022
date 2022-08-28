using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Part : MonoBehaviour
{
    public enum PartType { None, CPU, GPU, Memory, HDD, PSU }

    public PartType type;
    public int value = -1;

    private Rigidbody m_rigidbody;

    private Vector3 m_prevPosition;

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
            m_prevPosition = transform.position;
        }
    }

    public void StartGrab()
    {
        InGrab = true;
        m_rigidbody.isKinematic = true;
        m_rigidbody.useGravity = false;
    }

    public void EndGrab()
    {
        m_rigidbody.isKinematic = false;
        m_rigidbody.useGravity = true;
        InGrab = false;
    }
}
