using UnityEngine;
using System.Collections;
using OVR;

public class PinchTest : MonoBehaviour
{
    public OVRHand[] hands;
    public NV_rotation rotateable;

    private void Update()
    {
        bool inPinch = false;

        for (int i = 0; i < hands.Length; i++)
        {
            if(hands[i].GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                inPinch = true;
                break;
            }
        }

        rotateable.Animate = !inPinch;
    }
}
