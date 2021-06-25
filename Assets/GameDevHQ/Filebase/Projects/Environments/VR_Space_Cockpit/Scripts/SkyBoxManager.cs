using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{
    [SerializeField]
    Material[] Skybox;
    int currentBox;
    // Start is called before the first frame update
    void Start()
    {
        currentBox = 0;
        RenderSettings.skybox = Skybox[currentBox];
        
    }

   
    public void CycleSkybox()
    {
       
            currentBox++;
            if (currentBox >= Skybox.Length)
            {
                currentBox = 0;
            }
            RenderSettings.skybox = Skybox[currentBox];
           
        
    }
}
