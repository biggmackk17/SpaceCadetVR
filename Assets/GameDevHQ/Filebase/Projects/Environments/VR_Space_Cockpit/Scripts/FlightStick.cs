using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using UnityEngine.UI;


public class FlightStick : Grab
{
    [SerializeField]
 
   
    private float _rotationSpeed;
    [SerializeField]
    private float _stickRotationMultiplier;
    [SerializeField]
    private GameObject _ShipsRotation;
    private bool _autoPilot;

    private void Start()
    {
        _autoPilot = true;
    }


    protected override void Update()
    {
        if (!_autoPilot)
        base.Update();
    }

    protected override void OnRelease()
    {
        base.OnRelease();
        //transform.rotation = Quaternion.identity;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

       // transform.rotation = Quaternion.Euler(Vector3.zero);
       // transform.eulerAngles = Vector3.zero;
        Debug.Log("+++++++++++++++++++++++++++++++++++++++Local Rotation is " + transform.localRotation);
        Debug.Log("On Release");
    }
    
    protected override void WhileGrabbed()
    {
      
        {
            base.WhileGrabbed();
            var moved = _handPos - _grabOrigin;
            //this works but wont clamp
            // transform.LookAt(_hand.transform.position+transform.forward, Vector3.forward);


            var _moved = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) - _grabOrigin;


            _moved.z *= _rotationSpeed;
            _moved.x *= _rotationSpeed;

            transform.localRotation = Quaternion.Euler(_moved.z * _stickRotationMultiplier, 0, -_moved.x * _stickRotationMultiplier);
            _ShipsRotation.transform.Rotate(_moved.z, 0, -_moved.x);
            _ShipsRotation.transform.rotation = transform.rotation;
        }
    }
    protected override void OnGrab()
    {
       
        
            base.OnGrab();
        
    }

    private void OnEnable()
    {
        EventManager.OnDisableAutoPilot += DisableAutoPilot;
    }
  

    private void DisableAutoPilot()
    {
        _ShipsRotation.SetActive(true);
        _autoPilot = false;
    }
}

