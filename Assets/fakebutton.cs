using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakebutton : MonoBehaviour
{

    public delegate void Pressed();
    public static event Pressed OnPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Press();

        }
    }

    private void Press()
    {
        OnPressed?.Invoke();
    }
}
