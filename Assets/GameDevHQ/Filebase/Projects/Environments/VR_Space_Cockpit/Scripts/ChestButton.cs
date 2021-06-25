using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestButton : Switches
{
    [SerializeField]
    GameObject _chestCover;
    Animator _chestAnim;
    [SerializeField]
    GameObject _reactor;

   protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnReactorRemoval += EnableButton;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
       EventManager.OnReactorRemoval += EnableButton;
    }

    protected override void Start()
    {
        base.Start();
        _interactable = false;
        SetEmissionColor(Color.red);
        _chestAnim = _chestCover.GetComponent<Animator>();
    }

    

    protected override void OnLogic()
    {
        base.OnLogic();
        _chestAnim.SetTrigger("Open");
        _reactor.SetActive(true);
    }
}
