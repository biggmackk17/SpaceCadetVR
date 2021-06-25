using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffButton : Switches
{
    [SerializeField]
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnEnableTakeOff += EnableButton;
        EventManager.OnDisableAutoPilot += EnableButton;
    }
    
     
    
    protected override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnBootState -= EnableButton;
        EventManager.OnDisableAutoPilot -= EnableButton;
    }

    protected override void Start()
    {
        base.Start();
        OffLogic();
        _interactable = false;
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
    }


    protected override void EnableButton()
    {
        base.EnableButton();
    }



    protected override void OnLogic()
    {
        base.OnLogic();
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.green);
        _interactable = false;

        if (GameStateManager.Instance.currentState != GameStateManager.GameState.FreeFlight)
        {
            TimeLineManager.Instance.TakeOff();
        }
        else
        {

            EventManager.Instance.EndGame();
        }

    }

  



    protected override IEnumerator SwitchDelay() // ignores off delay;
    {
        yield return null;
    }

}
