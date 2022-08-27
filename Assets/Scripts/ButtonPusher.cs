using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ButtonPusher : MonoBehaviour
{
    public float pressRadius = 0.2f;

    [SerializeField] private OVRHand m_hand;
    [SerializeField] private LayerMask m_buttonLayer;

    private void Update()
    {
        var hits = Physics.OverlapSphere(m_hand.transform.position, pressRadius, m_buttonLayer);

        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<PushButton>().Push();
                break;
            }
        }
    }
}
