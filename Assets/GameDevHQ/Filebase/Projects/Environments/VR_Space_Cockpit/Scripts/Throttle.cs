using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;


public class Throttle : Grab
{
 
  [SerializeField]
  float _roationSpeed;
  [SerializeField]
  Transform _ship;
  ShipMovement _shipMovement;
  float _calculatedSpeed;
  float xRot;
  bool _autoPilot;


    private void OnEnable()
    {
        EventManager.OnDisableAutoPilot += DisableAutoPilot;
    }


    private void DisableAutoPilot()
    {
        _autoPilot = false;
    }


    private void Start()
    {
        _shipMovement = _ship.GetComponent<ShipMovement>();
        _autoPilot = true;
    }
    protected override void Update()
    {
        if (!_autoPilot)
        {
            base.Update();
            UpdateShipSpeed();
        }
    }
    private void UpdateShipSpeed()
    {
        if (!_autoPilot)
        {
            _shipMovement.SetSpeed(_calculatedSpeed);
        }
    }
    protected override void WhileGrabbed()
    {
        if (!_autoPilot)
        {
            base.WhileGrabbed();
            var _moved = _handPos - _grabOrigin;
            xRot += _moved.z * _roationSpeed * Time.deltaTime; 
            xRot = Mathf.Clamp(xRot, -50, 35);
            transform.localRotation = Quaternion.Euler(new Vector3(xRot, 0, 0));
            _calculatedSpeed = (xRot + 51) * .5f;
        }
    }
    protected override void OnGrab()
    {
        if (!_autoPilot)
        {
            base.OnGrab();
        }
    }

    public void AutoPilot(bool enabled)
    {
        _autoPilot = enabled;
    }



     

}
