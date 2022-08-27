using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    // TODO: Create part data and assign here

    public PartsFactory Owner { get; private set; }

    public void SetOwner(PartsFactory owner)
    {
        if (Owner == null) Owner = owner;
    }
}
