using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpButton : Switches
{

    private void OnEnable()
    {
        EventManager.OnEnableWarp += EnableButton;

    }

    private void OnDisable()
    {
        EventManager.OnEnableWarp += EnableButton;
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
        SetEmissionColor(Color.green);
        _interactable = false;
        TimeLineManager.Instance.Warp();
        StartCoroutine(FlipButtonDelay());
    }

    IEnumerator FlipButtonDelay()
    {
        yield return new WaitForSeconds(10);
        OffLogic();
        SetEmissionColor(Color.black);

    }

    protected override void EnableButton()
    {
        base.EnableButton();
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Takeoff)
        { GameStateManager.Instance.currentState = GameStateManager.GameState.Warp1; }
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Astroid)
        { GameStateManager.Instance.currentState = GameStateManager.GameState.Warp2; }
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Repair)
        { GameStateManager.Instance.currentState = GameStateManager.GameState.Warp3; }
    }

    protected override IEnumerator SwitchDelay()
    {
        yield return null;
    }
}
