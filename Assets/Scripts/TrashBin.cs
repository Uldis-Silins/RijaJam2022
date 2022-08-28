using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private int m_partLayer;

    private void Awake()
    {
        m_partLayer = LayerMask.NameToLayer("Part");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == m_partLayer)
        {
            Part part = other.GetComponent<Part>();

            if (part.InGrab) return;

            part.Owner.Despawn(part);
        }
    }
}
