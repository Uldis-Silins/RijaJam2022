using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public PartsFactory[] factories;

    private void Start()
    {
        factories[0].Spawn();
    }
}
