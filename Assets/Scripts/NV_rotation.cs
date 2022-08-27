using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NV_rotation : MonoBehaviour
{
    [SerializeField] float rvSpeed = 2.0f;

    public bool Animate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Animate)
        {
            transform.eulerAngles += new Vector3(0, rvSpeed * Time.deltaTime, 0);
        }
    }
}
