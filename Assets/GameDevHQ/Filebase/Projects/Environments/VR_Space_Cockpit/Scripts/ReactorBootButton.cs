using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBootButton : Switches
{

    override protected void OnEnable()
    {
        base.OnEnable();
        EventManager.OnReactorInstall += EnableButton;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnReactorInstall -= EnableButton;

    }

    protected override void Start()
    {
        base.Start();
        _interactable = false;
        SetEmissionColor(Color.black);
    }



    protected override void OnLogic()
    {
        base.OnLogic();
        EventManager.Instance.CountButtonPress();
        _interactable = false;
    }

    protected override IEnumerator SwitchDelay()
    {
        //void out switch delay
        yield return null;
    }
}
