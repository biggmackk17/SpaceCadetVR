using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : Switches
{

   
    
    // Start is called before the first frame update
   protected override void Start()
    {

        base.Start();
        
       
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.red);
    }

    protected override void OnLogic()
    {
        base.OnLogic();
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.green);

    }
    

    protected override void OffLogic()
    {
        base.OffLogic();
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.red);
    }

    

}




