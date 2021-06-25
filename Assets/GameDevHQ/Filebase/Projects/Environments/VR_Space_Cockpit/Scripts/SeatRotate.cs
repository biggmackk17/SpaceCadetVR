using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeatRotate : Switches
{
    Animator _imageAnimator;
    [SerializeField]
    GameObject _chair;
    [SerializeField]
    GameObject _image;
    bool _playDialogue;


    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnEnableSeatRotate += EnableButton;
        EventManager.OnReactorPowerOn += OnLogic;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnReactorPowerOn -= OnLogic;
        EventManager.OnEnableSeatRotate -= EnableButton;

    }

    protected override void Start()
    {
         
        base.Start();
        _playDialogue = true;
        _imageAnimator = _image.GetComponent<Animator>();
       
      

    }
    protected override void OnLogic()
    {
        base.OnLogic();
        StartCoroutine(RotateSeat());




    }


    IEnumerator RotateSeat()
    {
        _imageAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
       _chair.transform.Rotate(new Vector3(0, 180, 0));

        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Repair)
        {
            if (_playDialogue)
            {
                AudioManager.Instance.PlayDialogue(10, 1);
                _playDialogue = false;
            }
        }

        


    }


    protected override void OffLogic()
    {
        base.OffLogic();
        StartCoroutine(RotateSeat());
    }

    protected override void PowerUp()
    {
        base.PowerUp();
        _interactable = false;
    }

    protected override IEnumerator FlashButton()
    {

        _interactable = true;
        while (_on == false)
        {
            SetEmissionColor(Color.green);
            yield return new WaitForSeconds(.25f);
            SetEmissionColor(Color.black);
            yield return new WaitForSeconds(.25f);
        }
        SetEmissionColor(Color.green);
    }

}
