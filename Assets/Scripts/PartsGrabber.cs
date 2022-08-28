using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class PartsGrabber : MonoBehaviour
{
    public LevelController levelController;

    [SerializeField] private OVRHand hand;
    [SerializeField] private float grabRadius = 0.2f;
    [SerializeField] private LayerMask grabbableLayers;

    private List<Collider> m_prevHits;

    private Part m_currentGrabbedPart;
    private Vector3 m_grabOffset;

    private int m_endPinchCounter;
    private const float END_PINCH_BUFFER_COUNT = 15;

    private void Awake()
    {
        m_prevHits = new List<Collider>();
    }

    private void Update()
    {
        bool inPinch = hand.GetFingerIsPinching(OVRHand.HandFinger.Index) || hand.GetFingerIsPinching(OVRHand.HandFinger.Thumb) ||
            hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);

        if (m_currentGrabbedPart)
        {
            if (inPinch)
            {
                //m_currentGrabbedPart.transform.position = hand.transform.position + m_grabOffset;
                m_endPinchCounter = 0;
            }
            else
            {
                m_endPinchCounter++;

                if (m_endPinchCounter > END_PINCH_BUFFER_COUNT)
                {
                    m_currentGrabbedPart.transform.parent = null;
                    m_currentGrabbedPart.EndGrab();

                    m_currentGrabbedPart = null;
                }
            }
        }
        else
        {
            //for (int i = 0; i < m_prevHits.Count; i++)
            //{
            //    m_prevHits[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            //}

            //m_prevHits.Clear();

            Vector3 handPos = hand.transform.position;

            var hits = Physics.OverlapSphere(handPos, grabRadius, grabbableLayers);
            //m_prevHits.AddRange(hits);

            if (hits.Length > 0)
            {
                var closestPart = GetClosestPart(hand.transform.position, hits);

                if (closestPart != null)
                {
                    if (inPinch)
                    {
                        m_currentGrabbedPart = closestPart;
                        m_grabOffset = closestPart.transform.position - hand.transform.position;
                        m_currentGrabbedPart.transform.parent = hand.transform;
                        //closestPart.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                        closestPart.StartGrab();

                    }
                    else
                    {
                        //closestPart.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }
        }
    }

    private Part GetClosestPart(Vector3 position, Collider[] hits)
    {
        Collider closestHit = null;
        float closestDist = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {
            float dist = Vector3.Distance(hits[i].ClosestPoint(position), position);

            if(dist < closestDist)
            {
                closestDist = dist;
                closestHit = hits[i];
            }
        }

        if (closestHit == null) return null;

        return closestHit.GetComponent<Part>();
    }
}
