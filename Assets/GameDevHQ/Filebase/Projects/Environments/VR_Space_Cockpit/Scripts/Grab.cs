using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grab : MonoBehaviour
{
    [SerializeField]

    protected bool _grabbable;
    [SerializeField]
    protected bool _grabbed;
    [SerializeField]
    protected GameObject _hand;
    protected Vector3 _handPos;
    protected Vector3 _grabOrigin;
    protected enum HandGrab
    {
        RightGrab,
        LeftGrab,
    }
    private HandGrab handGrab;



    // private GameObject _hand;
    protected virtual void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);

        if (other.tag == "RightHand"&& _grabbed == false)
        {
            handGrab = HandGrab.RightGrab;
            _grabbable = true;
            _hand = other.gameObject;
        }
        if (other.tag == "LeftHand"&& _grabbed == false)
        {
            handGrab = HandGrab.LeftGrab;
            _grabbable = true;
            _hand = other.gameObject;
        }

    }
    protected virtual void OnTriggerExit(Collider other)
    {

        if (other.tag == "RightHand" && handGrab == HandGrab.RightGrab)
        {

            _grabbable = false;
        }

        if (other.tag == "LeftHand" && handGrab == HandGrab.LeftGrab)
        {

            _grabbable = false;
        }

    }
    protected virtual void Update()
    {
        OVRInput.Update();
        GrabbableCheck();
        Grabbed();



    }
    protected virtual void GrabbableCheck()
    {
        if (_grabbable)
        {



            if (handGrab == HandGrab.RightGrab)
            {

                if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) >= .5f)
                {
                    if (_grabbed == false)
                    {
                        _grabOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                        OnGrab();
                        _grabbed = true;
                    }
                }
                else
                {
                    _grabbed = false;
                    

                }
            }


          else if (handGrab == HandGrab.LeftGrab)
            {

                if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) >= .5f)
                {
                    if (_grabbed == false)
                    {
                      _grabOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                        OnGrab();
                        _grabbed = true;
                    }
                }
                else
                {
                    _grabbed = false;

                }
            }
        }
    }
    protected virtual void Grabbed()
    {
        if (_grabbed == true)
        {
            
            if (handGrab == HandGrab.RightGrab)
            {
                _handPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                WhileGrabbed();
                if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) <= .5f)
                {

                    _grabbed = false;
                    OnRelease();

                }
            }

            if (handGrab == HandGrab.LeftGrab)
            {
                _handPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                WhileGrabbed();
                Debug.Log("LEFT HAND INPUT!!!!!!!!!!! " + OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger));
                

                if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) <= .5f)
                {
                    _grabbed = false;
                    OnRelease();
                 

                }
            }

           

        }



    }
    protected virtual void WhileGrabbed()
    {
        Debug.Log("Grabbed");

    }
    protected virtual void OnRelease()
    {

        if (_hand != null)
        _hand.GetComponent<HandInteraction>().ShowHand(true);
       
    }
    protected virtual void OnGrab()
    {
        if (handGrab == HandGrab.LeftGrab)
        {
            _hand = GameObject.Find("CustomHandLeft");
            _hand.GetComponent<HandInteraction>().ShowHand(false);
        }
        if (handGrab == HandGrab.RightGrab)
        {
            _hand = GameObject.Find("CustomHandRight");
            _hand.GetComponent<HandInteraction>().ShowHand(false);
        }
    }






}
