using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGrabbable : MonoBehaviour
{
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.isKinematic == true)
        {
            transform.parent = null;
        }
    }
}
