using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootUpButton : Switches
{
    [SerializeField]
    AudioClip[] _bootClip;
    protected override void Start()
    {
        base.Start();
        _interactable = false;
        _on = false;
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
      
    }

    override protected void OnEnable()
    {
        base.OnEnable();
        EventManager.OnBootState += BootUpSequence;
    }
    override protected void OnDisable()
    {
        base.OnDisable();
        EventManager.OnBootState -= BootUpSequence;
    }

    public void EnableButton()
    {
        StartCoroutine(FlashButton());
    }

    public void DisableButton()
    {
        OffLogic();
        _interactable = false;
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
       

    }

    private void BootUpSequence()
    {
        StartCoroutine(FlashButton());
    }
   

    

    protected override void OnLogic()
    {
        base.OnLogic();
        EventManager.Instance.CountButtonPress(); // this lets the event manager know to add one to boot switch count 
        AudioManager.Instance.PlayEffect(_bootClip[Random.Range(0, _bootClip.Length)], .5f);
        _interactable = false;
    }

    protected override IEnumerator SwitchDelay() //this ignores standard switch delay
    {
        yield return null;
    }
}
