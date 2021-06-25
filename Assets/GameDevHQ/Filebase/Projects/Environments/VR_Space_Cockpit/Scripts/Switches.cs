using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switches : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    protected bool _on;
    [SerializeField]
    protected bool _interactable;
    [SerializeField]
    protected float _delayTime = .5f;
    [SerializeField]
    protected AudioClip _Onclip;
    [SerializeField]
    protected AudioClip _Offclip;
    [SerializeField]
    protected AudioClip _Errorclip;
    protected Material _glowMat;

    protected Color _savedColor;
    protected bool _savedInteractable;



    protected virtual void OnEnable()
    {
        EventManager.OnPowerDown += PowerDown;
        EventManager.OnPowerUp += PowerUp;
    }
    protected virtual void OnDisable()
    {
        EventManager.OnPowerDown -= PowerDown;
        EventManager.OnPowerUp -= PowerUp;
    }


    public virtual void Switch()
    {
        if (_interactable)
        {

            if (_on)
            {

                OffLogic();

            }
            else
            {

                OnLogic();

            }

        }
        else ErrorLogic();

    }



    protected virtual void ErrorLogic()
    {

    }

    protected virtual void OffLogic()
    {
        _on = false;
        _anim.SetBool("On", false);
        AudioManager.Instance.PlayEffect(_Offclip, 1);
        StartCoroutine(SwitchDelay());

    }

    protected virtual void OnLogic()
    {
        _on = true;
        _anim.SetBool("On", true);
        AudioManager.Instance.PlayEffect(_Onclip, 1);
        StartCoroutine(SwitchDelay());
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _anim = transform.GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError(transform.name + " The Animator Is Null");
        }
        _glowMat = transform.GetComponent<MeshRenderer>().materials[0];

        _interactable = true;


    }

    protected virtual void SetEmissionColor(Color newcolor)
    {
     _glowMat.SetColor("_EmissionColor", newcolor);
        
    }


   protected virtual IEnumerator FlashButton()
    {
        OffLogic();
        _interactable = true;
        while (_on == false)
        {
            SetEmissionColor(Color.red);
            yield return new WaitForSeconds(.25f);
            SetEmissionColor(Color.black);
            yield return new WaitForSeconds(.25f);
        }
        SetEmissionColor(Color.green);
    }



    protected virtual IEnumerator SwitchDelay()
    {
        _interactable = false;
        yield return new WaitForSeconds(_delayTime);
        _interactable = true;
    }


    protected virtual void EnableButton()
    {
        StartCoroutine(FlashButton());
    }


    protected virtual void PowerDown()
    {
        _savedColor = _glowMat.GetColor("_EmissionColor");
        SetEmissionColor(Color.black);
        _savedInteractable = _interactable;
        _interactable = false;

    }
    protected virtual void PowerUp()
    {
        SetEmissionColor(_savedColor);
        _interactable = _savedInteractable;
        Debug.Log("powerUpCalled");
        Debug.Log(_savedColor);

    }
}

