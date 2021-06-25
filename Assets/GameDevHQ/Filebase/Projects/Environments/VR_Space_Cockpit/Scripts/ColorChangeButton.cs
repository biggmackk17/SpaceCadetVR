using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeButton : Switches
{

    protected override void Start()
    {
        base.Start();
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.red);
    }

    protected override void OnLogic()
    {
        base.OnLogic();
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.blue);
    }
    protected override void OffLogic()
    {
        base.OffLogic();
    
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.cyan);
    }
}
