using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDrive : MonoBehaviour
{
    [SerializeField]
    GameObject _workingReactor;
    [SerializeField]
    GameObject _brokenReactor;
    [SerializeField]
    GameObject _sparks;
    AudioSource _reactorSource;
    [SerializeField]
    AudioClip _runningClip, _BrokenClip;
    bool _firstDetect;

    private void Start()
    {
        _reactorSource = transform.GetComponent<AudioSource>();
        _firstDetect = true;
    }

    private void OnEnable()
    {
       
        EventManager.OnPowerUp += BreakReactor;
        EventManager.OnReactorPowerOn += PowerOnReactor;
        EventManager.OnEnableTakeOff += PlayEngineSound;
        EventManager.OnPowerDown += StopEngineSound;
    }
    private void OnDisable()
    {
        EventManager.OnPowerUp -= BreakReactor;
        EventManager.OnReactorPowerOn -= PowerOnReactor;
        EventManager.OnEnableTakeOff -= PlayEngineSound;
        EventManager.OnPowerDown -= StopEngineSound;
    }

    private void PowerOnReactor()
    {
       _workingReactor.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        PlayEngineSound();

    }

    public void StopEngineSound()
    {
        _reactorSource.Stop();
    }

    public void PlayEngineSound()
    {
        _reactorSource.clip = _runningClip;
        _reactorSource.Play();
    }

   public void BreakReactor()
    {
        _workingReactor.SetActive(false);
        _brokenReactor.SetActive(true);
        _sparks.SetActive(true);
        _reactorSource.clip = _BrokenClip;
        _reactorSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        var reactor = other.GetComponent<Reactor>();
        if (reactor != null)
        {
            if (reactor.BrokenCheck()&& _firstDetect)
            {
                _firstDetect = false;
                reactor.TurnOff();
                _sparks.SetActive(false);
                _reactorSource.Stop();
                AudioManager.Instance.PlayDialogue(11, 1);
                EventManager.Instance.ReactorRemoved();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var reactor = other.GetComponent<Reactor>();
        if (reactor != null)
        {
            if (!reactor.BrokenCheck())
            {
               // other.gameObject.SetActive(false);
                StartCoroutine(ReplaceReactor());
                //AudioManager.Instance.PlayDialogue(12, 1);
            }
        }
    }


    [SerializeField]
    GameObject _newReactor;
    IEnumerator ReplaceReactor()
    {
      
        var __grabbedObject =  _newReactor.GetComponent<OVRGrabbable>();
        Debug.Log("OVR GRABBABLE" + __grabbedObject.name);
        var __grabber = __grabbedObject.grabbedBy;
        if (__grabber != null)
        {
            __grabber.ForceRelease(__grabbedObject);
        }
        // __grabbedObject.GrabEnd(Vector3.zero,Vector3.zero);
        yield return new WaitForSeconds(.1f);

       _brokenReactor.SetActive(false);
        _newReactor.SetActive(false);
        _workingReactor.SetActive(true);
        _workingReactor.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        StartCoroutine(ReactorInstalledDialogue());
       





    }
    IEnumerator ReactorInstalledDialogue()
    {
        AudioManager.Instance.PlayDialogue(12, 1f);
        while (AudioManager.Instance.DialogueCheck())
        {
            yield return new WaitForSeconds(.25f);
        }
        EventManager.Instance.ReactorInstalled();
       

    }

    void StartReactor()
    {
        _workingReactor.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        _reactorSource.clip = _runningClip;
        _reactorSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
