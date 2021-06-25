using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMaterial : MonoBehaviour
{
    MeshRenderer _mesh;
    [SerializeField]
    float power = 0; 
    

    private void Start()
    {
        _mesh = transform.GetComponent<MeshRenderer>();
        _mesh.material.SetFloat("_Power", power);
        
    }

    private void Update()
    {
        _mesh.material.SetFloat("_Power", power);
    }


}
