using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class PartsGrabber : MonoBehaviour
{
    [SerializeField] private OVRHand[] hands;
    [SerializeField] private float grabRadius = 0.7f;
    [SerializeField] private LayerMask grabbableLayers;

    private void Update()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            Vector3 handPos = hands[i].transform.position;

            var hits = Physics.OverlapSphere(handPos, grabRadius, grabbableLayers);

            if (hands[i].GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                if (hits.Length > 0)
                {
                    for (int j = 0; j < hits.Length; j++)
                    {
                        Debug.Log("Hits[" + i + "]: " + hits[i].gameObject.name);
                        hits[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }
        }
    }
}
