using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipMovement : MonoBehaviour
{
    Quaternion _targetRotation;
    [SerializeField]
    Transform _shipsRotation;
    [SerializeField]
    float _lerpSpeed=.5f;
    [SerializeField]
    float _shipSpeed = 10;
    Rigidbody _rb;
    [SerializeField]
    private Transform GrabbableObjects;
    bool _autoPilot;




    private void OnEnable()
    {
        EventManager.OnDisableAutoPilot += DisableAutoPilot;
    }


    private void DisableAutoPilot()
    {
        _autoPilot = false;
        _shipSpeed = 10f; ;
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _autoPilot = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!_autoPilot)
        {
            _targetRotation = _shipsRotation.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _lerpSpeed);
            Physics.SyncTransforms();
        }
        //transform.Translate(transform.forward*_shipSpeed, Space.World);
       

       
    }
    Vector3 _LastFrameVel = Vector3.zero;
    private void FixedUpdate()
    {
    
         _rb.velocity = transform.forward * _shipSpeed;
   
    }
    [SerializeField]
    float speedT;
    public void SetSpeed(float targetSpeed)
    {
        //_shipSpeed = targetSpeed;
       _shipSpeed =  Mathf.Lerp(_shipSpeed, targetSpeed, speedT* Time.deltaTime);
       

    }
}
