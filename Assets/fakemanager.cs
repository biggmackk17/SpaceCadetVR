using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakemanager : MonoBehaviour
{

    int _amountPressed;

    private void OnEnable()
    {
        fakebutton.OnPressed += CountPressed;
    }

    private void OnDisable()
    {
        fakebutton.OnPressed -= CountPressed;
    }

    private void CountPressed()
    {
        _amountPressed++;

        if (_amountPressed > 16)
        {

            //fire off the next event 
        }


    }
}
